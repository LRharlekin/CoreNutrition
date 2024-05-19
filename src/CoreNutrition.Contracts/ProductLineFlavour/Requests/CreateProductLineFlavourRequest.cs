namespace CoreNutrition.Contracts.ProductLineFlavour;

public record CreateProductLineFlavourRequest(
  string Flavour,
  string ProductLineId,
  string FlavourImageUrl
);