using System.Net;
using API.Data.Models;
using Microsoft.Extensions.Options;
using API.Services.Interfaces;
using API.Services.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace API.Services
{
    public class ApiService(ISendGridClient sendGridClient, IOptions<ApiOptions> opt) : IApiService
    {
        private readonly ApiOptions _options = opt.Value;

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
    }
}