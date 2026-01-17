using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveX.Core.Application.Features.User.Commands.CreateUser;
using ReserveX.Core.Application.Features.User.Commands.SetStatus;
using ReserveX.Core.Application.Features.User.Queries;
using ReserveX.Core.Application.Features.User.Queries.GetUserById;
using ReserveX.Core.Application.Features.User.Queries.GetUserListPaged;

namespace WebApi.Controllers.v1
{
    [Authorize(Roles =("Admin"))]
    [Route("api/{version::apiVersion}/users")]
    public class UsersController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand request)
        {
            var userId = await _mediator.Send(request);

            return CreatedAtAction(
                nameof(GetUserById),
                new { userId },
                null
            );
        }


        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute]GetUserByIdQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetUserList(GetUserListPagedQuery request)
        {
           var result= await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{userId:guid}/status")]
        public async Task<IActionResult> SetUserStatus(SetUserStatusCommand request)
        {
            var result = await _mediator.Send(request);
            return NoContent();
        }

        [HttpGet("{userId:guid}/reservations")]
        public async Task<IActionResult> GetUserReservations(GetAllUserReservationsQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
