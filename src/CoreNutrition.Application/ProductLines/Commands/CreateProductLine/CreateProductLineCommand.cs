using ErrorOr;

using CoreNutrition.Application.Common.Security;
using CoreNutrition.Domain.ProductLineAggregate;

namespace CoreNutrition.Application.ProductLines.Commands.CreateProductLine;

[Authorize(Roles = "Admin")]
public record CreateProductLineCommand(
  string Name,
  string CategoryId,
  bool IsPublished,
  // initial average rating = null,
  ProductLineInfoCommand ProductLineInfo,
  NutritionFactsCommand NutritionFacts
  )
  : IAuthorizeableAction<ErrorOr<ProductLine>>;

public record ProductLineInfoCommand(
  string DescriptionShort,
  string DescriptionLong,
  string SuggestedUse,
  string Benefit1,
  string Benefit2,
  string Benefit3,
  bool IsMuscleGain,
  bool IsWeightLoss,
  bool IsHealthWellness
);

public record NutritionFactsCommand(
  double CaloriesPer100Grams,
  double FatPer100Grams,
  double SaturatedFatPer100Grams,
  double CarbohydratesPer100Grams,
  double SugarPer100Grams,
  double ProteinPer100Grams,
  double SaltPer100Grams
);