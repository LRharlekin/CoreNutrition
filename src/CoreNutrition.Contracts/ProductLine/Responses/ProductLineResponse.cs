namespace CoreNutrition.Contracts.ProductLine;

public record ProductLineResponse(
  string Id,
  string Name,
  string CategoryId,
  bool IsPublished,
  AverageRatingResponse AverageRating,
  ProductLineInfoResponse ProductLineInfo,
  NutritionFactsResponse NutritionFacts,
  List<string> ProductLineSizeIds,
  List<string> ProductLineFlavourIds,
  List<string> ProductIds,
  DateTime CreatedDateTime,
  DateTime UpdatedDateTime
);

public record AverageRatingResponse(
  double Value,
  int NumRatings
);

public record ProductLineInfoResponse(
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

public record NutritionFactsResponse(
  double CaloriesPer100Grams,
  double FatPer100Grams,
  double SaturatedFatPer100Grams,
  double CarbohydratesPer100Grams,
  double SugarPer100Grams,
  double ProteinPer100Grams,
  double SaltPer100Grams
);