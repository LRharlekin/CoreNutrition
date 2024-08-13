# Domain Aggregate: Product Line Size

## Product Line Size C# Class and Behaviors

```csharp
class ProductLineSize
{
    // TODO: Add methods
}
```

## Product Line Size JSON Object

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "productLineId": "00000000-0000-0000-0000-000000000000",
  "sizeVariant": {
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "1 Bar (60g)",
    "units": 1,
    "unitWeightInGrams": 60,
    "unitVolumeInMilliliters": null,
    "singleSizeId": null
  },
  "recommendedRetailPrice": {
    "amount": 2.99,
    "currencyCode": "EUR"
  },
  "createdDateTime": "2024-01-01T00:00:00.0000000Z",
  "updatedDateTime": "2024-01-01T00:00:00.0000000Z"
}
```

## Tables

**ProductLineSizes**

- Id
- ProductLineId (FK, not null)
- SizeVariantId (FK, not null)
- RecommendedRetailPrice_Amount (not null)
- RecommendedRetailPrice_CurrencyCode (not null)
- CreatedDateTime
- UpdatedDateTime

**SizeVariants**

- Id (PK, not null)
- Name (not null, nvarchar(200))
- Units (not null, int)
- UnitWeightInGrams ()
- UnitVolumeInMilliliters
- SingleSizeVariantId (FK)

**PlsProductIds**

- ProductId
- ProductLineSizeId
- Id
