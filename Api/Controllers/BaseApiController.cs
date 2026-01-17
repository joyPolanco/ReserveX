using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReserveX.Core.Application;
using ReserveX.Core.Domain.Common.enums;

namespace WebApi.Controllers
{
    [Route("api/v{version::apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator _mediator=> HttpContext!.RequestServices!.GetService<IMediator>()!;

        protected BaseApiController()
        {
        }


        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.BadRequest => BadRequest(result.Error),
                    ErrorType.Conflict => Conflict(result.Error),
                    ErrorType.NotFound => NotFound(result.Error),
                    ErrorType.Unauthorized => Unauthorized(result.Error),
                    _ => StatusCode(500, result.Error)
                };
            
            }
            if (result.Value == null || result.Value.Equals(default(T)))
               return NoContent();
            return Ok(result.Value);
        }
    }
}
