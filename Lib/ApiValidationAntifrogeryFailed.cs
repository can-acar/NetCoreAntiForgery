using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace Lib
{
    public class ApiValidationAntifrogeryFailed : BadRequestResult, IAntiforgeryValidationFailedResult, IActionResult
    {
        public ApiValidationAntifrogeryFailed()
        {
        }

        public override void ExecuteResult(ActionContext context)
        {
           
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var Response = context.HttpContext.Response;
            var ErrorsObject = new JObject();
            var Serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            ErrorsObject["message"] = "Cheater!.. Get off  here!.";

            var Error = JObject.FromObject(ErrorsObject, Serializer);

            Response.StatusCode = StatusCode;

            Response.WriteAsync(Error.ToString());

            return base.ExecuteResultAsync(context);
        }
    }
}