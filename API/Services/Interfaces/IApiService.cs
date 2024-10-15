using Fulfill3D.API.API.Data.Models;

namespace Fulfill3D.API.API.Services.Interfaces
{
    public interface IApiService
    {
        Task<bool> SendEmail(FormRequest formRequest);
        Task<IEnumerable<Post>> GetPublishedBlogPostsMetadata();
        Task<Post> GetBlogPost(string id, string partitionKey);
        Task<IEnumerable<Project>> GetProjectsMetadata();
        Task<Project> GetProject(string id, string partitionKey);
        Task<Person> GetPerson(string id, string partitionKey);
        Task<Company> GetCompany(string id, string partitionKey);
    }
}