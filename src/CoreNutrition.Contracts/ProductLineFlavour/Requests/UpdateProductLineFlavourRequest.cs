namespace CoreNutrition.Contracts.ProductLineFlavour;

public record UpdateProductLineFlavourRequest(
string? Flavour,
string? ProductLineId,
string? FlavourImageUrl
);