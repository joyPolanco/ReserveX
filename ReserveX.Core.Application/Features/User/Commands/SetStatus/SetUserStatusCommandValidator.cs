using FluentValidation;


namespace ReserveX.Core.Application.Features.User.Commands.SetStatus
{
    public class SetUserStatusCommandValidator: AbstractValidator<SetUserStatusCommand>
    {
        public SetUserStatusCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().WithMessage("UserId is required");
            RuleFor(x => x.ToActive).NotNull().WithMessage("Status indicator is required");

        }
    }
}
