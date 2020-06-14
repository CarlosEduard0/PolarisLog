using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PolarisLog.WebApi.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var erros = context.ModelState.Values.SelectMany(entry => entry.Errors)
                    .Select(error => error.ErrorMessage);
                context.Result = new BadRequestObjectResult(erros);

                return;
            }

            await next();
        }
    }
}