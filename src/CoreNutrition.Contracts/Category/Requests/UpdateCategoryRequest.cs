namespace CoreNutrition.Contracts.Category;

public record UpdateCategoryRequest(
string? Name,
string? Description,
string? CategoryImageUrl
);