using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using API.Data.Models;
using API.Services;
using API.Services.Interfaces;
using API.Services.Options;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;

namespace API
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            Action<RecaptchaOptions> configureRecaptchaOptions,
            Action<ApiOptions> configureApiOptions,
            Action<SendGridClientOptions> configureSendGrid)
        {
            #region Miscellaneous

            services.ConfigureServiceOptions<RecaptchaOptions>((_, opt) => configureRecaptchaOptions(opt));
            services.ConfigureServiceOptions<ApiOptions>((_, opt) => configureApiOptions(opt));
            services.AddHttpRequestBodyMapper();
            services.AddFluentValidator<FormRequest>();
            
            services.AddHttpClient("RecaptchaClient", client =>
            {
                client.BaseAddress = new Uri("https://www.google.com/recaptcha/api/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            
            services.AddSendGrid((_, options) => configureSendGrid(options));

            #endregion

            #region Services

            services.AddTransient<IApiService, ApiService>();
            services.AddTransient<IRecaptchaService, RecaptchaService>();

            #endregion
        }

        private static void ConfigureServiceOptions<TOptions>(
            this IServiceCollection services,
            Action<IServiceProvider, TOptions> configure)
            where TOptions : class
        {
            services
                .AddOptions<TOptions>()
                .Configure<IServiceProvider>((options, resolver) => configure(resolver, options));
        }

        private static void AddHttpRequestBodyMapper(this IServiceCollection services)
        {
            services.AddTransient(typeof(IHttpRequestBodyMapper<>), typeof(HttpRequestBodyMapper<>));
        }

        private static void AddFluentValidator<T>(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(T).Assembly);
        }
    }
}