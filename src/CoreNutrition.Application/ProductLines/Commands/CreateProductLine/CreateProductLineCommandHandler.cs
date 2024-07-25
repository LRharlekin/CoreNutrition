using MediatR;
using ErrorOr;

using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.CategoryAggregate.ValueObjects;
using CoreNutrition.Domain.Common.ValueObjects;

namespace CoreNutrition.Application.ProductLines.Commands.CreateProductLine;

internal sealed class CreateProductLineCommandHandler
  : IRequestHandler<CreateProductLineCommand, ErrorOr<ProductLine>>
{
  private readonly IProductLineRepository _productLineRepository;

  public CreateProductLineCommandHandler(
    IProductLineRepository productLineRepository
  )
  {
    _productLineRepository = productLineRepository;
  }

  public async Task<ErrorOr<ProductLine>> Handle(
    CreateProductLineCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    Guid.TryParse(command.CategoryId, out var categoryIdGuid);
    CategoryId categoryId = CategoryId.Create(categoryIdGuid);

    ErrorOr<ProductLine> productLineResult = ProductLine.Create(
      command.Name,
      command.IsPublished,
      categoryId,
      AverageRating.CreateNew().Value,
      ProductLineInfo.CreateNew(
        command.ProductLineInfo.DescriptionShort,
        command.ProductLineInfo.DescriptionLong,
        command.ProductLineInfo.SuggestedUse,
        command.ProductLineInfo.Benefit1,
        command.ProductLineInfo.Benefit2,
        command.ProductLineInfo.Benefit3,
        command.ProductLineInfo.IsMuscleGain,
        command.ProductLineInfo.IsWeightLoss,
        command.ProductLineInfo.IsHealthWellness
      ).Value,
      NutritionFacts.CreateNew(
        command.NutritionFacts.CaloriesPer100Grams,
        command.NutritionFacts.FatPer100Grams,
        command.NutritionFacts.SaturatedFatPer100Grams,
        command.NutritionFacts.CarbohydratesPer100Grams,
        command.NutritionFacts.SugarPer100Grams,
        command.NutritionFacts.ProteinPer100Grams,
        command.NutritionFacts.SaltPer100Grams
      ).Value
    );

    if (productLineResult.IsError)
    {
      return productLineResult.Errors;
    }

    // _productLineRepository.Add(productLineResult.Value);

    // return productLineResult.Value;
    return productLineResult.Value;
  }
}