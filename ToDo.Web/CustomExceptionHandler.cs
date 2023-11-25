using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Common.Exceptions;

namespace ToDo.Web
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails(validationException.Errors)
                {
                    Status = StatusCodes.Status400BadRequest
                }, cancellationToken);

                return true;
            } else if (exception is NotFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                await httpContext.Response.WriteAsync(exception.Message, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
