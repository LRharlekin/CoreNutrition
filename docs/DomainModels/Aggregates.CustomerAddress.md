# Domain Aggregate: Customer Address

## Customer Address C# Class and Behaviors

```csharp
class CustomerAddress
{
    // TODO: Add methods
}
```

## Customer Address JSON Object

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "customerId": "00000000-0000-0000-0000-000000000000",
  "address": {
    "streetName": "Mannerheimintie",
    "streetNumber": "132",
    "unitNumber": "B 39",
    "addressLine1": "c/o John Wick",
    "addressLine2": "(Chief of Staff of Governors Office)",
    "postalCode": "00620",
    "city": "Helsinki",
    "region": "", // "Region / Province / State" field on form
    "country": {
      "id": "FIN", // ISO 3166 Alpha-3 country codes
      "name": "Finland"
    }
  },
  "createdDateTime": "2024-01-01T00:00:00.0000000Z",
  "updatedDateTime": "2024-01-01T00:00:00.0000000Z"
}
```
