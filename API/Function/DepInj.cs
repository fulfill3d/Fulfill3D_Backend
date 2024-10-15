using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Fulfill3D.API.API.Data.Models;
using Fulfill3D.API.API.Services;
using Fulfill3D.API.API.Services.Interfaces;
using Fulfill3D.API.API.Services.Options;
using Fulfill3D.Integrations.CosmosDbClient;
using Fulfill3D.Integrations.CosmosDbClient.Options;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;

namespace Fulfill3D.API.API
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            Action<RecaptchaOptions> configureRecaptchaOptions,
            Action<EmailMetaOptions> configureApiOptions,
            Action<SendGridClientOptions> configureSendGrid,
            Action<CosmosDbClientOptions> configureCosmosDbClientOptions)
        {
            #region Miscellaneous

            services.ConfigureServiceOptions<RecaptchaOptions>((_, opt) => configureRecaptchaOptions(opt));
            services.ConfigureServiceOptions<EmailMetaOptions>((_, opt) => configureApiOptions(opt));
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
            services.RegisterCosmosDbClient(configureCosmosDbClientOptions);

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