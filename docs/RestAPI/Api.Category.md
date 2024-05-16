# Category API Endpoints Documentation

**Table of Contents**

- [Category API Endpoints Documentation](#category-api-endpoints-documentation)
  - [Write Models / Commands](#write-models--commands)
    - [Create Category](#create-category)
      - [Create Category Request](#create-category-request)
      - [Create Category Response](#create-category-response)
  - [Read Models / Queries](#read-models--queries)
    - [List all Categories](#list-all-categories)
      - [Get All Categories Request](#get-all-categories-request)
      - [Get All Categories Response](#get-all-categories-response)

## Write Models / Commands

### Create Category

```csharp
[Authorize(Roles="admin")]
[HttpPost("categories")]
```

#### Create Category Request

```json
{
  "name": "Protein",
  "description": "Protein is a must for muscle maintenance, growth, and recovery, as well as simply ensuring you get enough protein throughout the day. And whether you add it to your morning oats or whip up a satisfying shake, protein powder is a fast and easy way to up your protein intake. There are loads of protein powders out there. From whey protein to vegan protein, meal replacers and more, there's a flavour and formulation for you. Better yet, protein powder can help you shed a little extra weight if that’s your goal.",
  "categoryImageUrl": "https://www.imgur.com/1203948alfkjdhs0319487khjafs"
}
```

#### Create Category Response

```js
201 CREATED
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "Protein",
  "description": "Protein is a must for muscle maintenance, growth, and recovery, as well as simply ensuring you get enough protein throughout the day. And whether you add it to your morning oats or whip up a satisfying shake, protein powder is a fast and easy way to up your protein intake. There are loads of protein powders out there. From whey protein to vegan protein, meal replacers and more, there's a flavour and formulation for you. Better yet, protein powder can help you shed a little extra weight if that’s your goal.",
  "categoryImageUrl": "https://www.imgur.com/1203948alfkjdhs0319487khjafs",
  "productLineIds": [],
  "createdDateTime": "2024-05-13T00:00:00",
  "updatedDateTime": "2024-05-13T00:00:00"
}
```

## Read Models / Queries

### List all Categories

```csharp
[Authorize(Roles="admin")]
[HttpGet("categories")]
```

#### Get All Categories Request

```json
{}
```

#### Get All Categories Response

```js
200 OK
```

```json
{
  [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "category": {
        "name": "Protein",
        "description": "Protein is a must for muscle maintenance, growth, and recovery, as well as simply ensuring you get enough protein throughout the day. And whether you add it to your morning oats or whip up a satisfying shake, protein powder is a fast and easy way to up your protein intake. There are loads of protein powders out there. From whey protein to vegan protein, meal replacers and more, there's a flavour and formulation for you. Better yet, protein powder can help you shed a little extra weight if that’s your goal.",
        "categoryImageUrl": "https://www.imgur.com/1203948alfkjdhs0319487khjafs",
        "productLineIds": []
      }
    },
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "category": {}
    },
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "category": {}
    }
  ]
}
```
