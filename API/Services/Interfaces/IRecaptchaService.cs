namespace API.Services.Interfaces
{
    public interface IRecaptchaService
    {
        Task<bool> Validate(string recaptcha);
    }
}