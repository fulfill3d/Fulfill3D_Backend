using System.Net;
using Fulfill3D.API.API.Data.Models;
using Microsoft.Extensions.Options;
using Fulfill3D.API.API.Services.Interfaces;
using Fulfill3D.API.API.Services.Options;
using Fulfill3D.Integrations.CosmosDbClient.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Fulfill3D.API.API.Services
{
    public class ApiService(ICosmosDbClient cosmosDbClient, ISendGridClient sendGridClient, IOptions<EmailMetaOptions> opt) : IApiService
    {
        private readonly EmailMetaOptions _options = opt.Value;

        public async Task<bool> SendEmail(FormRequest formRequest)
        {
            var msg = new SendGridMessage();
            var emailOptions = new EmailOptions(formRequest.Email, formRequest.Subject, formRequest.Message);
            var sendGridRecipients = new List<EmailAddress>
            {
                new ()
                {
                    Name = _options.ReceiverName,
                    Email = _options.EmailTo
                }
            };

            msg.SetFrom(new EmailAddress(_options.EmailFrom, _options.SenderName));
            msg.AddTos(sendGridRecipients);
            msg.SetSubject(_options.EmailSubject);
            
            msg.AddContent(MimeType.Text, emailOptions.Message);

            var response = await sendGridClient.SendEmailAsync(msg);
            
            return response.StatusCode == HttpStatusCode.Accepted;
        }
        
        public async Task<IEnumerable<Post>> GetPublishedBlogPostsMetadata()
        {
            string query = @"SELECT c.id, c.partitionKey, c.title, c.slug, c.author, c.tags, c.excerpt, c.image, c.status FROM c WHERE c.status = 'published'";
            return await cosmosDbClient.QueryItemsAsync<Post>(query);
        }
        
        public async Task<Post> GetBlogPost(string id, string partitionKey)
        {
            return await cosmosDbClient.GetItemAsync<Post>(id, partitionKey);
        }
        
        public async Task<IEnumerable<Project>> GetProjectsMetadata()
        {
            string query = @"SELECT * FROM c WHERE c.status = 'active'";
            return await cosmosDbClient.QueryItemsAsync<Project>(query);
        }
        
        public async Task<Project> GetProject(string id, string partitionKey)
        {
            return await cosmosDbClient.GetItemAsync<Project>(id, partitionKey);
        }

        public async Task<Person> GetPerson(string id, string partitionKey)
        {
            return await cosmosDbClient.GetItemAsync<Person>(id, partitionKey);
        }

        public async Task<Company> GetCompany(string id, string partitionKey)
        {
            return await cosmosDbClient.GetItemAsync<Company>(id, partitionKey);
        }
    }
}