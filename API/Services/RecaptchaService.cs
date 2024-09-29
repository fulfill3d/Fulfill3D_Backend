using API.Data.Models;
using API.Services.Interfaces;
using API.Services.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Services
{
    public class RecaptchaService(IHttpClientFactory httpClientFactory, IOptions<RecaptchaOptions> opt): IRecaptchaService
    {
        private readonly string _recaptchaSecret = opt.Value.Secret;
        private readonly HttpClient _client = httpClientFactory.CreateClient("RecaptchaClient");
        
        public async Task<bool> Validate(string recaptcha)
        {
            var recaptchaVerificationUrl = $"siteverify?secret={_recaptchaSecret}&response={recaptcha}";
            var recaptchaResponse = await _client.GetStringAsync(recaptchaVerificationUrl);
            var recaptchaResult = JsonConvert.DeserializeObject<RecaptchaValidationResult>(recaptchaResponse);
            
            return recaptchaResult is { Success: true, Score: >= 0.5 };
        }
    }
}