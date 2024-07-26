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

    // 0. prepare immutable value objects
    Guid.TryParse(command.CategoryId, out var categoryIdGuid);
    CategoryId categoryId = CategoryId.Create(categoryIdGuid);

    var averageRating = AverageRating.CreateNew().Value;

    Console.WriteLine("AFTER .CreateNew() in handler");
    Console.WriteLine($"average rating: {averageRating.Score}");

    ErrorOr<ProductLineInfo> productLineInfoResult = ProductLineInfo.CreateNew(
        command.ProductLineInfo.DescriptionShort,
        command.ProductLineInfo.DescriptionLong,
        command.ProductLineInfo.SuggestedUse,
        command.ProductLineInfo.Benefit1,
        command.ProductLineInfo.Benefit2,
        command.ProductLineInfo.Benefit3,
        command.ProductLineInfo.IsMuscleGain,
        command.ProductLineInfo.IsWeightLoss,
        command.ProductLineInfo.IsHealthWellness
      );

    if (productLineInfoResult.IsError)
    {
      return productLineInfoResult.Errors;
    }

    ErrorOr<NutritionFacts> nutritionFactsResult = NutritionFacts.CreateNew(
      command.NutritionFacts.CaloriesPer100Grams,
      command.NutritionFacts.FatPer100Grams,
      command.NutritionFacts.SaturatedFatPer100Grams,
      command.NutritionFacts.CarbohydratesPer100Grams,
      command.NutritionFacts.SugarPer100Grams,
      command.NutritionFacts.ProteinPer100Grams,
      command.NutritionFacts.SaltPer100Grams
    );

    if (nutritionFactsResult.IsError)
    {
      return nutritionFactsResult.Errors;
    }

    // 1. create
    ErrorOr<ProductLine> productLineResult = ProductLine.Create(
      command.Name,
      command.IsPublished,
      categoryId,
      averageRating,
      productLineInfoResult.Value,
      nutritionFactsResult.Value
    );

    if (productLineResult.IsError)
    {
      return productLineResult.Errors;
    }

    // 2. persist
    _productLineRepository.Add(productLineResult.Value);

    // 3. return
    return productLineResult.Value;
  }
}