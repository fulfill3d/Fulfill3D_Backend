namespace Fulfill3D.API.API.Services.Interfaces
{
    public interface IRecaptchaService
    {
        Task<bool> Validate(string recaptcha);
    }
}