using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
