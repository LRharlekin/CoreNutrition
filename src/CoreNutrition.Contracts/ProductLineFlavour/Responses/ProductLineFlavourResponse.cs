namespace CoreNutrition.Contracts.ProductLineFlavour;

public record ProductLineFlavourResponse(
  string Id,
  string Flavour,
  string FlavourImageUrl,
  string? ProductLineId, // FK
  List<string> ProductIds, // related entity with FK in the other entity
  DateTime CreatedDateTime,
  DateTime UpdatedDateTime
);