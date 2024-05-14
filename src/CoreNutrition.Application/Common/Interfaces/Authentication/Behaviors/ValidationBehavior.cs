using ErrorOr;
using FluentValidation;
using MediatR;

namespace CoreNutrition.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> // requests come from mediator
        where TResponse : IErrorOr // result object with success and/or error(s)
{
  private readonly IValidator<TRequest>? _validator; // 0 or 1 validator, might need to change to accept multiple?

  public ValidationBehavior(IValidator<TRequest>? validator = null) // 0 or 1 validator, might need to change to accept multiple?
  {
    _validator = validator;
  }

  public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next, // 0 or 1 validator, if multiple: iterate through multiple here before calling delegate
      CancellationToken cancellationToken)
  {
    if (_validator is null)
    {
      return await next();
    }

    var validationResult = await _validator.ValidateAsync(request, cancellationToken);

    if (validationResult.IsValid)
    {
      return await next();
    }

    var errors = validationResult.Errors
        .ConvertAll(validationFailure => Error.Validation(
            validationFailure.PropertyName,
            validationFailure.ErrorMessage));

    // dynamic cast at runtime to TResponse, which is ErrorOr<T>
    return (dynamic)errors;
  }
}
