
using Cart.API.Application.Response;
using FluentValidation;
using FluentValidation.Results;

namespace Cart.API.Application.UseCases
{
    public abstract class UseCase<Inp, Out> : IUseCase<Inp, Out>
    {
        public string[] GetAllErrors(ValidationResult validationResult) =>
        validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToArray();

        public ValidationResult ValidateEntity<TValidator, TEntity>(TValidator validation, TEntity entity)
            where TValidator : AbstractValidator<TEntity>
            where TEntity : class => validation.Validate(entity);

        public abstract Task<Response<Out>> HandleAsync(Inp input);
    }
}
