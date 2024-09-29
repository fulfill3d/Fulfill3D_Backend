using API.Data.Models;

namespace API.Services.Interfaces
{
    public interface IApiService
    {
        Task<bool> SendEmail(FormRequest formRequest);
    }
}