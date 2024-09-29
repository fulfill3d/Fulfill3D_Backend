using System.Net;
using API.Data.Models;
using API.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace API
{
    public class ApiFunction(
        IRecaptchaService recaptchaService,
        IHttpRequestBodyMapper<FormRequest> formRequestBodyMapper,
        IApiService apiService
        )
    {
        [Function(nameof(SendFormRequest))]
        public async Task<HttpResponseData> SendFormRequest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "send-form-request")]
            HttpRequestData req,
            FunctionContext executionContext)
        {
            var response = req.CreateResponse();
            var request = await formRequestBodyMapper.Map(req.Body);
            
            var valid = await recaptchaService.Validate(request.ReCaptcha);
            
            if (!valid)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }
            
            var success = await apiService.SendEmail(request);
            
            if (!success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}