using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class ExceptionFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is CQRSException exception)
            {
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = (exception as CQRSException).StatusCode,
                };
                context.ExceptionHandled = true;
            }
        }

    }
}