using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            _problemDetailsService = problemDetailsService;
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
     HttpContext httpContext,
     Exception exception,
     CancellationToken cancellationToken)
        {
            string title = "Unexpected error";
            int status = (int)HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case KeyNotFoundException:
                    title = "Not found";
                    status = (int)HttpStatusCode.NotFound;
                    break;

                case ArgumentException:
                    title = "Bad request";
                    status = (int)HttpStatusCode.BadRequest;
                    break;

                case SqlException:
                case DbUpdateException:
                case TimeoutException:
                case NullReferenceException:
                case InvalidOperationException:
                    title = "Internal Server Error";
                    status = (int)HttpStatusCode.InternalServerError;
                    break;

                case HttpRequestException:
                    title = "Service Unavailable";
                    status = (int)HttpStatusCode.ServiceUnavailable;
                    break;

                case SecurityTokenException:
                    title = "Corrupted token";
                    status = (int)HttpStatusCode.Unauthorized;
                    break;

                default:
                    break;
            }

            httpContext.Response.StatusCode = status;
            httpContext.Response.ContentType = "application/problem+json";

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Detail = exception.Message, 
                    Title = title,
                    Type = exception.GetType().Name,
                    Status = status
                }
            });
        }

    }
}
