using MediatR;
using ErrorOr;

using CoreNutrition.Domain.ProductLineSizeAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.Common.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.ProductLineSizeAggregate.Entities;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;

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

    // 1. create
    //
    var recommendedRetailPrice = CurrencyAmount.CreateNew(
      amount: command.RecommendedRetailPrice.Amount,
      currencyCode: command.RecommendedRetailPrice.CurrencyCode);

    var sizeVariant = SizeVariant.Create(
      name: command.SizeVariant.Name,
      units: command.SizeVariant.Units,
      unitWeightInGrams: command.SizeVariant.UnitWeightInGrams,
      unitVolumeInMilliliters: command.SizeVariant.UnitVolumeInMilliliters,
      singleSizeVariantId: SizeVariantId.Create(command.SizeVariant.SingleSizeVariantId).Value // returns ErrorOr<>


  ErrorOr < ProductLineSize > productLineSizeResult = ProductLineSize.Create(
    productLineId: ProductLineId.Create(command.ProductLineId).Value, // returns ErrorOr<>
    recommendedRetailPrice,
    sizeVariant
    )
  );


    if (productLineSizeResult.IsError)
    {
      return productLineSizeResult.Errors; // errors bybass mapping pipeline
    }

    // 2. persist
    _productLineSizeRepository.Add(productLineSizeResult.Value);

    // 3. return
    //     return productLineSizeResult.Value;
    return default!;
  }
}