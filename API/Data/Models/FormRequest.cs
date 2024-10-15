using FluentValidation;
using Newtonsoft.Json;

namespace Fulfill3D.API.API.Data.Models
{
    public class FormRequest
    {
        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("subject")] public string Subject { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("recaptcha")] public string ReCaptcha { get; set; }
    }

    public class RequestValidator : AbstractValidator<FormRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(100).WithMessage("Subject must be less than or equal to 100 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(1000).WithMessage("Message must be less than or equal to 1000 characters.");

            RuleFor(x => x.ReCaptcha)
                .NotEmpty().WithMessage("ReCaptcha is required.");
        }
    }
}