using FluentValidation.Results;


namespace ReserveX.Core.Application.Exceptions
{
    public class InputValidationException: Exception
    {
        public List<string> Errors { get; set; }
        public InputValidationException() : base("Error occurs")
        {
            Errors = [];
        }
        public InputValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);

            }
        }
    }
}
