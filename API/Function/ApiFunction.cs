using System.Net;
using Fulfill3D.API.API.Data.Models;
using Fulfill3D.API.API.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace Fulfill3D.API.API
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
        
        [Function(nameof(GetPublishedBlogPostsMetadata))]
        public async Task<HttpResponseData> GetPublishedBlogPostsMetadata(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-published-posts")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var posts = await apiService.GetPublishedBlogPostsMetadata();

            await response.WriteStringAsync(JsonConvert.SerializeObject(posts, Formatting.Indented));
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function(nameof(GetPostById))]
        public async Task<HttpResponseData> GetPostById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "post/{id}")]
            HttpRequestData req, string id)
        {
            var response = req.CreateResponse();
            var posts = await apiService.GetBlogPost(id, "posts");

            await response.WriteStringAsync(JsonConvert.SerializeObject(posts, Formatting.Indented));
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function(nameof(GetProjectsMetadata))]
        public async Task<HttpResponseData> GetProjectsMetadata(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-active-projects")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var posts = await apiService.GetProjectsMetadata();

            await response.WriteStringAsync(JsonConvert.SerializeObject(posts, Formatting.Indented));
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function(nameof(GetProjectById))]
        public async Task<HttpResponseData> GetProjectById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "project/{id}")]
            HttpRequestData req, string id)
        {
            var response = req.CreateResponse();
            var posts = await apiService.GetProject(id, "projects");

            await response.WriteStringAsync(JsonConvert.SerializeObject(posts, Formatting.Indented));
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function(nameof(GetPerson))]
        public async Task<HttpResponseData> GetPerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-person")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var posts = await apiService.GetPerson("abdurrahman_gazi_yavuz", "person");

            await response.WriteStringAsync(JsonConvert.SerializeObject(posts, Formatting.Indented));
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function(nameof(GetCompany))]
        public async Task<HttpResponseData> GetCompany(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-company")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var posts = await apiService.GetCompany("fulfill3d", "company");

            await response.WriteStringAsync(JsonConvert.SerializeObject(posts, Formatting.Indented));
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}