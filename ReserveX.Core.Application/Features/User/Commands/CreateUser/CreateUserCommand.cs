using MediatR;
using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Application.Helpers;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Core.Domain.Common.enums;
using ReserveX.Core.Domain.Interfaces;

namespace ReserveX.Core.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result<Guid>>
    {
        public  string? Name { get; set; }
        public string? Password { get; set; }

        public string ?LastName { get; set; }
        public  string? Email { get; set; }
        public  string? Role { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,Result< Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;

        public CreateUserCommandHandler(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository=userRepository;
            _hasher= hasher;
        }
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var uniqueEmail = !await _userRepository.GetAllQuery().Where(x => x.Email == request.Email).AnyAsync();

            if (!uniqueEmail) return Result<Guid>.Failure("Email associated to other account", ErrorType.Conflict);
            var created = await _userRepository.AddAsync(new Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Email = request.Email!,
                LastName = request.LastName!,
                Name = request.Name!,
                PasswordHash = _hasher.Hash(request.Password!),
                CreatedAt = DateTime.UtcNow,
                Status=Status.ACTIVE,
                Role = EnumHelper.GetEnumValueFromString<UserRole>(request.Role!)
            });
            return Result<Guid>.Success(created!.Id);

        }
    }
}
