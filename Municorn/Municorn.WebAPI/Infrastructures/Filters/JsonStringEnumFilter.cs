using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Municorn.WebAPI.Infrastructures.Filters
{
    public class JsonStringEnumFilter : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objResult)
            {
                var value = objResult.Value;

                var serializerSettings = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                serializerSettings.Converters.Add(new JsonStringEnumConverter());
                context.Result = new JsonResult(value, serializerSettings);
            }

            await next();
        }
    }
}