# Auth API Endpoints Documentation

**Table of Contents**

- [Auth API Endpoints Documentation](#auth-api-endpoints-documentation)
  - [Register](#register)
    - [Register Request](#register-request)
    - [Register Response](#register-response)
  - [Login](#login)
    - [Login Request](#login-request)
    - [Login Response](#login-response)

## Register

```js
POST "/auth/register";
```

### Register Request

```json
{
  "firstName": "Arnold",
  "lastName": "Schwarzenegger",
  "email": "aschwarzenegger@email.fi",
  "password": "123456AaBbCc?!"
}
```

### Register Response

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

## Login

```js
POST "/auth/login";
```

### Login Request

```json
{
  "email": "aschwarzenegger@email.fi",
  "password": "123456AaBbCc?!"
}
```

### Login Response

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
