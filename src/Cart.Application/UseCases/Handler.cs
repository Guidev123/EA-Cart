using FluentValidation;
using FluentValidation.Results;

namespace Cart.Application.UseCases
{
    public abstract class Handler
    {
        public static string[] GetAllErrors(ValidationResult validationResult) =>
        validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToArray();
        public static void AddError(ValidationResult validationResult, string message) =>
           validationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        public static ValidationResult ValidateEntity<TValidator, TEntity>(TValidator validation, TEntity entity)
            where TValidator : AbstractValidator<TEntity>
            where TEntity : class => validation.Validate(entity);
    }
}
