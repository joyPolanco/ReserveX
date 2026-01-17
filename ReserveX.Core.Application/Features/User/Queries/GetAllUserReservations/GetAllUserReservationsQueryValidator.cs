using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application.Features.User.Queries.GetAllUserReservations
{
    public class GetAllUserReservationsQueryValidator: AbstractValidator<GetAllUserReservationsQuery>
    {
        public GetAllUserReservationsQueryValidator()
        {
            RuleFor(r => r.UserId).NotEmpty().WithMessage("UserId is required");
            When(r => r.Date.HasValue, () =>
            {
                RuleFor(r => r.Date)
              .Must(d => d.Value >= DateOnly.FromDateTime(DateTime.Now))
              .WithMessage("Date must be today or later");
            });
            When(r => r.StartTime.HasValue && r.EndTime.HasValue, () =>
            {
                RuleFor(r => r)
                    .Must(r => r.StartTime < r.EndTime)
                    .WithMessage("StartTime must be less than EndTime");
            });

        }
    }
}
