using FluentValidation;
using ReserveX.Core.Domain.Common.enums;
using ReserveX.Core.Domain.Interfaces;


namespace ReserveX.Core.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>

    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email format is required");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required")
                .MinimumLength(5)
                .WithMessage("LastName must have at least 5 characters");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must have at least 2 characters");

            RuleFor(x => x.Role).Must(role=> Enum.TryParse<UserRole>(role,true,out _))
                .WithMessage("Invalid Role");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must have at least 8 characters");
            RuleFor(x => x.Password)
                .Matches("[A-Z]")
                    .WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]")
                    .WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]")
                    .WithMessage("Password must contain at least one numeric digit.")
                .Matches("[^a-zA-Z0-9]")
                    .WithMessage("Password must contain at least one special character.");




        }
    }
}
