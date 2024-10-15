using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Models
{
    public class Response
    {
        [JsonProperty("value")] public string Value { get; set; }
    }
    
    public class RecaptchaValidationResult
    {
        public bool Success { get; set; }
        public double Score { get; set; }
        public string Action { get; set; }
        public DateTime ChallengeTs { get; set; }
        public string Hostname { get; set; }
        public List<string> ErrorCodes { get; set; }
    }
}