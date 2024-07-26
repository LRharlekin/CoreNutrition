using FluentValidation;

using CoreNutrition.Domain.ProductAggregate;
using System.Linq.Expressions;

namespace CoreNutrition.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator
  : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(command => command.Name)
      .NotNull()
      .NotEmpty()
      .Length(Product.Constraints.MinNameLength, Product.Constraints.MaxNameLength);

    RuleFor(command => command.RetailPrice.Amount)
      .NotNull()
      .GreaterThan(0)
      .GreaterThan(Product.Constraints.MinRetailPrice);

    RuleFor(command => command.QuantityInStock)
      .NotNull()
      .GreaterThanOrEqualTo(0)
      .GreaterThanOrEqualTo(Product.Constraints.MinQuantityInStock);

    RuleFor(command => command.ProductImageUrl)
      .NotNull()
      .NotEmpty()
      .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
      .WithMessage("The Product Image URL is not a valid URL.");

    ApplyGuidValidationRule(command => command.ProductLineId);
    ApplyGuidValidationRule(command => command.ProductLineSizeId);
    ApplyGuidValidationRule(command => command.ProductLineFlavourId);

    ApplyBooleanRules();
  }

  private void ApplyBooleanRules()
  {
    var booleanProperties = new[]
    {
                nameof(CreateProductCommand.IsPublished),
                nameof(CreateProductCommand.IsVegan),
                nameof(CreateProductCommand.IsSample)
            };

    foreach (var propName in booleanProperties)
    {
      RuleFor(command => typeof(CreateProductCommand).GetProperty(propName).GetValue(command))
          .NotNull()
          .Must(value => value is bool)
          .WithMessage($"{propName} must be a boolean value.");
    }
  }

  private void ApplyGuidValidationRule(Expression<Func<CreateProductCommand, string>> propertyExpression)
  {
    var guidPattern = @"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";

    RuleFor(propertyExpression)
      .NotNull()
      .NotEmpty()
      .Length(36)
      .Matches(guidPattern);
  }
}