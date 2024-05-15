# Auth API Endpoints Documentation

**Table of Contents**

- [Auth API Endpoints Documentation](#auth-api-endpoints-documentation)
  - [Create Category](#create-category)
    - [Create Category Request](#create-category-request)
    - [Create Category Response](#create-category-response)

## Create Category

```js
POST "/categories";
```

### Create Category Request

```json
{
  "firstName": "Arnold",
  "lastName": "Schwarzenegger",
  "email": "aschwarzenegger@email.fi",
  "password": "123456AaBbCc?!"
}
```

### Create Category Response

```js
200 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "firstName": "Arnold",
  "lastName": "Schwarzenegger",
  "email": "aschwarzenegger@email.fi",
  "token": "aBcDeF...1234567890"
}
```
