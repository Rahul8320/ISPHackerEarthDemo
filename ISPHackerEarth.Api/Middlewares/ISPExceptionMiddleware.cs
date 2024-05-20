using ISPHackerEarth.Domain.Common.Exceptions;
using ISPHackerEarth.Domain.Common.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ISPHackerEarth.Api.Middlewares;

public class ISPExceptionMiddleware(ILoggerService logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ISPException)
        {
            var ispException = (exception as ISPException)!;
            logger.LogError(ispException.Message, exception: ispException);

            httpContext.Response.StatusCode = (int)ispException.StatusCode;
            var problemDetails = new ProblemDetails
            {
                Detail = ispException.Message,
                Status = (int)ispException.StatusCode,
                Title = "Something went worng!",
            };
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        }
        else
        {
            logger.LogError(exception.Message, exception: exception);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error!",
            };
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        }

        return true;
    }
}
