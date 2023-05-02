using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text;
using WebApplication5.Common.Services;

namespace WebApplication5.Filters
{
    public class RequestBodyFilter : Attribute, IResourceFilter
    {
        private ICryptoService _cryptoService;

        public RequestBodyFilter(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        /// <summary>
        /// Read the encrypted request body and decrypt Then bound to the from body
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var request = context.HttpContext.Request;
            if (request.Body.CanRead)
            {
                var reader = new StreamReader(request.Body);
                var requestBody = reader.ReadToEndAsync().Result.ToString();
                requestBody = JsonConvert.DeserializeObject<string>(requestBody); 
                if (!string.IsNullOrEmpty(requestBody))
                {
                    var decryptedText = _cryptoService.Decrypt(requestBody);
                    byte[] bytes = Encoding.ASCII.GetBytes(decryptedText);
                    request.Body = new MemoryStream(bytes);
                }
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //Do Nothing
        }
    }
}
