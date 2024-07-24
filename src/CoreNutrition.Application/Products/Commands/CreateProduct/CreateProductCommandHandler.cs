using MediatR;
using ErrorOr;

using CoreNutrition.Domain.Common.ValueObjects;
using CoreNutrition.Domain.Common.DomainErrors;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

using CoreNutrition.Domain.ProductAggregate;
using CoreNutrition.Domain.ProductAggregate.ValueObjects;

using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;

namespace CoreNutrition.Application.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler
  : IRequestHandler<CreateProductCommand, ErrorOr<Product>>

{
  // private readonly IProductRepository _productRepository;

  // public CreateProductCommandHandler
  // (
  //   IProductRepository productRepository
  // )
  // {
  //   _productRepository = productRepository;
  // }


  public async Task<ErrorOr<Product>> Handle(
    CreateProductCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    var retailPrice = CurrencyAmount.CreateNew(
      amount: command.RetailPrice.Amount,
      currencyCode: command.RetailPrice.CurrencyCode);

    Guid.TryParse(command.ProductLineId, out var productLineIdGuid);
    ProductLineId productLineId = ProductLineId.Create(productLineIdGuid);

    Guid.TryParse(command.ProductLineSizeId, out var productLineSizeIdGuid);
    ProductLineSizeId productLineSizeId = ProductLineSizeId.Create(productLineSizeIdGuid);

    Guid.TryParse(command.ProductLineFlavourId, out var productLineFlavourIdGuid);
    ProductLineFlavourId productLineFlavourId = ProductLineFlavourId.Create(productLineFlavourIdGuid);

    Uri.TryCreate(command.ProductImageUrl, UriKind.Absolute, out var productImageUrl);

    // create
    ErrorOr<Product> productResult = Product.Create(
      name: command.Name,
      isPublished: command.IsPublished,
      averageRating: AverageRating.CreateNew().Value,
      retailPrice,
      quantityInStock: command.QuantityInStock,
      productLineId,
      productLineSizeId,
      productLineFlavourId,
      isVegan: command.IsVegan,
      isSample: command.IsSample,
      productImageUrl!
    );

    if (productResult.IsError)
    {
      return productResult.Errors;
    }

    // persist

    return productResult.Value;
  }
}