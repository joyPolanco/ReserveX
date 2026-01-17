using MediatR;
using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Application.Helpers;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Core.Application.Services;
using ReserveX.Core.Domain.Common.enums;
using ReserveX.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Features.Auth.Commands.SignIn
{
    public class SignInCommand : IRequest<Result<Guid>>
    {
        public string? Name { get; set; }
        public string? Password { get; set; }

        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
    public class SignInCommandHandler : IRequestHandler<SignInCommand,Result< Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;

        public SignInCommandHandler(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }
        public async Task<Result<Guid>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
             var uniqueEmail=!await _userRepository.GetAllQuery().Where(x => x.Email == request.Email).AnyAsync();

            if (!uniqueEmail) return Result<Guid>.Failure("Email associated to other account", ErrorType.Conflict);
        var created = await _userRepository.AddAsync(new Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Email = request.Email!,
                LastName = request.LastName!,
                Name = request.Name!,
                PasswordHash = _hasher.Hash(request.Password!),
                CreatedAt = DateTime.UtcNow,
                Status = Status.ACTIVE,
                Role = UserRole.USER
            });
            return Result<Guid>.Success(created!.Id);
        }
    }
}
