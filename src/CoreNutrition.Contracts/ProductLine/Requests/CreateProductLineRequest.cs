namespace CoreNutrition.Contracts.ProductLine;

public record CreateProductLineRequest(
  string Name,
  string CategoryId,
  bool IsPublished,
  // initial average rating = null,
  ProductLineInfoRequest ProductLineInfo,
  NutritionFactsRequest NutritionFacts
);

public record ProductLineInfoRequest(
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

public record NutritionFactsRequest(
  double CaloriesPer100Grams,
  double FatPer100Grams,
  double SaturatedFatPer100Grams,
  double CarbohydratesPer100Grams,
  double SugarPer100Grams,
  double ProteinPer100Grams,
  double SaltPer100Grams
);