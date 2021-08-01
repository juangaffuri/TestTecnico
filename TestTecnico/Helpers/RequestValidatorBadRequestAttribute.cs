using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestTecnico.ViewModel;
using TestTecnico.Extensions;

namespace TestTecnico.Helpers
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if ((modelState != null) && (!modelState.IsValid))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var jsonString = JsonSerializer.Serialize<ResponseError>(new ResponseError()
                {
                    CodigoError = "400",
                    Detalle = modelState.FormatErrorBadRequest()
                }, options);
                context.Result = new ContentResult()
                {
                    Content = jsonString,
                    StatusCode = 400,
                    ContentType = "application/json"
                };
            }
            base.OnActionExecuting(context);
        }
    }
    
}
