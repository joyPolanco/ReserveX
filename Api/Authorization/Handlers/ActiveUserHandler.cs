using Microsoft.AspNetCore.Authorization;
using ReserveX.Core.Application.Authorization.Requirements;
using ReserveX.Core.Domain.Interfaces;

namespace WebApi.Authorization.Handlers
{
    public class ActiveUserHandler
        : AuthorizationHandler<ActiveUserRequirement>
    {
        private readonly IUserRepository _userRepository;

        public ActiveUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ActiveUserRequirement requirement)
        {
            var userId = context.User.FindFirst("sub")?.Value;

            if (userId == null)
                return;

            Guid.TryParse(userId, out var guid);
            var isActive = await _userRepository.UserIsActive(guid);

            if (isActive)
                context.Succeed(requirement);
        }
    }
    }
