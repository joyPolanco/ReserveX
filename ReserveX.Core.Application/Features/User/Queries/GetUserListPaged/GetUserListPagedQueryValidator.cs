using FluentValidation;

namespace ReserveX.Core.Application.Features.User.Queries.GetUserListPaged
{
    public class GetUserListPagedQueryValidator: AbstractValidator<GetUserListPagedQuery>
    {
        public GetUserListPagedQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page number must be equal or greater than 1");
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize  must be equal or greater than 1");


        }
    }
}
