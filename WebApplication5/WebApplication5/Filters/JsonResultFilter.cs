using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication5.Common.Services;

namespace WebApplication5.Filters
{
    public class JsonResultFilter : ActionFilterAttribute
    {
        private ICryptoService _cryptoService;

        public JsonResultFilter(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result as ObjectResult;
            if (result == null)
            {
                context.Result = new ObjectResult(new BadRequestResult());
            }
            else if (result.StatusCode == 200)
            {
                var encryptedResult = _cryptoService.Encrypt(result.Value.ToString());
                context.Result = new OkObjectResult(encryptedResult);
            }
            else if (result.StatusCode != null)
            {
                context.Result = new StatusCodeResult(result.StatusCode.Value);
            }
            else if (result.StatusCode == null && result.Value != null)
            {
                context.Result = new ObjectResult(result.Value.ToString());
            }
        }
    }
}
