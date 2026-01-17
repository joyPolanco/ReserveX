using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace ReserveX.Core.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
 

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(c => c.ValidateAsync(validationContext, cancellationToken)));
                var failures = validationResults.SelectMany(e => e.Errors).Where(f => f != null).ToList();
                if (failures.Count > 0)
                {
                    var errors = failures.Select(f => f.ErrorMessage).ToList();
                    object failure= Result<TResponse>.Failure(string.Join(";", errors), Domain.Common.enums.ErrorType.BadRequest);
                    return (TResponse)failure;
                }
            }
            return await next(cancellationToken);
        }
    }
}
