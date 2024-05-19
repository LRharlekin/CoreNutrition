using MediatR;
using ErrorOr;

using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Application.ProductLineSizes.Commands.CreateProductLineSize;

internal sealed class CreateProductLineSizeCommandHandler
  : IRequestHandler<CreateProductLineSizeCommand, ErrorOr<ProductLineSize>>
{
  private readonly IProductLineSizeRepository _productLineSizeRepository;

  public CreateProductLineSizeCommandHandler(
    IProductLineSizeRepository productLineSizeRepository
  )
  {
    _productLineSizeRepository = productLineSizeRepository;
  }

  public async Task<ErrorOr<ProductLineSize>> Handle(
    CreateProductLineSizeCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    ErrorOr<ProductLineSize> productLineSizeResult = ProductLineSize.Create(
      // command.SizeVariant,
      SizeId command.SizeVariantId,
    ProductLineId command.ProductLineId,
    CurrencyAmount command.RecommendedRetailPrice
    );

    if (productLineSizeResult.IsError)
    {
      return productLineSizeResult.Errors; // errors bybass mapping pipeline
    }

    _productLineSizeRepository.Add(productLineSizeResult.Value);

    return productLineSizeResult.Value;
  }
}