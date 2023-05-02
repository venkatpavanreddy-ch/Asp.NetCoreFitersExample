using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        protected static AuthSettings _authSettings;
        private readonly IConfiguration _configuration;
        public ChatController(AuthSettings authSettings, IConfiguration configuration)
        {
            _authSettings = authSettings;
            _configuration = configuration;
        }

        [HttpPost("Conversations")]
        public async Task<IActionResult> Conversations([FromBody] Request request)
        {
            var token = GetToken(request);
            if (token != null)
            {
                var _httpClient = new HttpClient();
                var clientapi = _configuration.GetValue<string>("ClientAPIURL");
                string requestUrl = $"{clientapi}/conversations/";
                _httpClient.DefaultRequestHeaders.Add("Authorization", new AuthenticationHeaderValue("bearer", token.access_token).ToString());

                var response = _httpClient.GetAsync(requestUrl).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return new ObjectResult(response);
                }
            }
            return BadRequest();
        }

        [HttpPost("Messages")]
        public async Task<IActionResult> Messages([FromBody] Request request)
        {
            var token = GetToken(request);
            if (token != null)
            {
                var _httpClient = new HttpClient();
                var clientapi = _configuration.GetValue<string>("ClientAPIURL");
                string requestUrl = $"{clientapi}/conversations/{request.ConversationID}/messages";
                _httpClient.DefaultRequestHeaders.Add("Authorization", new AuthenticationHeaderValue("bearer", token.access_token).ToString());

                var response = _httpClient.GetAsync(requestUrl).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpPost("Message")]
        public async Task<IActionResult> GetMessage([FromBody] Request request)
        {
            var token = GetToken(request);
            if (token != null)
            {
                var _httpClient = new HttpClient();
                var clientapi = _configuration.GetValue<string>("ClientAPIURL");
                string requestUrl = $"{clientapi}/conversations/{request.ConversationID}/messages/{request.MessageID}";
                _httpClient.DefaultRequestHeaders.Add("Authorization", new AuthenticationHeaderValue("bearer", token.access_token).ToString());

                var response = _httpClient.GetAsync(requestUrl).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpPost("CreateMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageRequest request)
        {
            var token = GetToken(request);
            if (token != null)
            {
                var httpClient = new HttpClient();
                var clientapi = _configuration.GetValue<string>("ClientAPIURL");
                string requestUrl = $"{clientapi}/conversations/{request.ConversationID}/messages";
                httpClient.DefaultRequestHeaders.Add("Authorization", new AuthenticationHeaderValue("bearer", token.access_token).ToString());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                httpRequest.Content = new StringContent(CreateMessageObject(request), Encoding.UTF8, "application/json");
                var response = httpClient.SendAsync(httpRequest).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        private AuthToken GetToken(Request request)
        {
            string requestUrl = $"{_authSettings.url}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            httpRequest.Content = new FormUrlEncodedContent(GetTokenParameters(request));
            var httpClient = new HttpClient();
            var response = httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AuthToken>(content);
            }
            return null;
        }

        private static IEnumerable<KeyValuePair<string, string>> GetTokenParameters(Request request)
        {
            var paramList = new List<KeyValuePair<string, string>>();
            paramList.Add(new KeyValuePair<string, string>("grant_type", _authSettings.grant_type));
            paramList.Add(new KeyValuePair<string, string>("username", request.UserName));
            paramList.Add(new KeyValuePair<string, string>("password", request.Password));
            paramList.Add(new KeyValuePair<string, string>("audience", _authSettings.audience));
            paramList.Add(new KeyValuePair<string, string>("scope", _authSettings.scope));
            paramList.Add(new KeyValuePair<string, string>("client_id", _authSettings.client_id));
            paramList.Add(new KeyValuePair<string, string>("client_secret", _authSettings.client_secret));
            return paramList;
        }

        private static string CreateMessageObject(MessageRequest request)
        {
            string content = $"\"content\":\"{request.Message}\", \"priority\":{request.Priority}, \"messageType\":{request.MessageType}, \"contentType\":{request.ContentType}";
            var metaData = JsonConvert.SerializeObject(request.MetaData);
            content = content + $",\"metadata\":{metaData}";
            return "{" + content + "}";
        }
    }
}
