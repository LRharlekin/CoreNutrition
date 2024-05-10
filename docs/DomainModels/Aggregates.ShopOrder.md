# Domain Aggregate: Shop Order

## Shop Order C# Class and Behaviors

```csharp
class ShopOrder
{
    // TODO: Add methods
}
```

## Shop Order JSON Object

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "customer": {
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "Arnold",
    "lastName": "Schwarzenegger",
    "email": "a.schwarzenegger@email.fi"
  },
  "addresses": [
    {
      "customerAddressId": "00000000-0000-0000-0000-000000000000",
      "isShippingAddress": true,
      "isBillingAddress": false,
      "address": {
        "streetName": "Mannerheimintie",
        "streetNumber": "132",
        "unitNumber": "B 39",
        "addressLine1": "c/o John Wick",
        "addressLine2": "(Chief of Staff of Governors Office)",
        "postalCode": "00620",
        "region": "",
        "city": "Helsinki",
        "country": {
          "id": "FIN", // ISO 3166 Alpha-3 country codes
          "name": "Finland"
        }
      }
    },
    {
      "customerAddressId": "00000000-0000-0000-0000-000000000000",
      "isShippingAddress": false,
      "isBillingAddress": true,
      "address": {
        "streetName": "Beverly Hills Blvd.",
        "streetNumber": "1666",
        "unitNumber": "",
        "addressLine1": "",
        "addressLine2": "",
        "postalCode": "12345",
        "city": "Los Angeles",
        "region": "California",
        "country": {
          "id": "USA", // ISO 3166 Alpha-3 country codes
          "name": "United States of America"
        }
      }
    }
  ],
  "orderLineItems": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "productId": "00000000-0000-0000-0000-000000000000",
      "quantity": 4,
      "pricePerUnit": {
        "amount": 39.99,
        "currencyCode": "EUR",
        "currency": "Euro"
      }
    },
    {
      // more order line items
    }
  ],
  "orderStatus": "delivered", // enum: confirmed, processed, shipped, delivered, cancelled, returned, lost
  "shippingMethod": {
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "DHL", // Fedex Express, Posti
    "price": {
      "amount": 4.99,
      "currencyCode": "EUR", // ISO 4217 Currency Codes
      "currency": "Euro"
    }
  },
  "shippingCost": 0.0, // != shippingMethod.price --> free shipping with promotions or above specific total order value
  "discountCode": {
    "id": "00000000-0000-0000-0000-000000000000",
    "code": "MRBEAST15",
    "discountRate": 0.15
  },
  "totalOrderValue": {
    "amount": 135.97,
    /*
      In this example:
      (4 * 39.99 EUR) // order line items' quantity * price per unit
      * (1 - 0.15) // discount code applied
      + 0.00 EUR // free shipping
      = 135.97
      */
    "currencyCode": "EUR", // ISO 4217 Currency Codes
    "currency": "Euro"
  },
  "orderDateTime": "2024-01-01T00:00:00.0000000Z",
  "createdAt": "2024-01-01T00:00:00.0000000Z",
  "updatedAt": "2024-01-01T00:00:00.0000000Z"
}
```
