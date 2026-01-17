using Microsoft.AspNetCore.Diagnostics;
using ReserveX.Core.Application.Exceptions;
using System.Net;

namespace WebApi.Handlers
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger _logger;
            public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger)
        {

            _problemDetailsService=problemDetailsService;
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            string title = "Unexpected error";
            int status = (int)HttpStatusCode.InternalServerError;
            string details = exception.Message;


            switch (exception)
            {
                case KeyNotFoundException:
                    title = "Not found";
                    status = (int)HttpStatusCode.NotFound;
                    break;
                case ArgumentException:
                    status = (int)HttpStatusCode.BadRequest;
                    title = "Bad request";
                    break;

                case InvalidCredentialsException:
                    status =(int)HttpStatusCode.Unauthorized;

                    break;

                case InputValidationException:
                    status = (int)HttpStatusCode.BadRequest;
                    title = "Bad request";
                    details = ((ReserveX.Core.Application.Exceptions.InputValidationException)exception).Errors.Aggregate((a, b) => a + "," + b);
                    break;
                default:
                  status = (int)HttpStatusCode.InternalServerError;
                  details = exception.Message;
                    break;

            }

            var problem = new
            {
                Title = title,
                Details = details,
                Status = status,
                Instance = httpContext.Request.Path
            };


            httpContext.Response.StatusCode = status;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: cancellationToken);

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext= httpContext,
                Exception= exception,
                ProblemDetails= new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Detail= details,
                    Title= "An error occured",
                    Type = exception.GetType().Name
                }
            });
        }
    }
}
