# Auth API Endpoints Documentation

**Table of Contents**

- [Auth API Endpoints Documentation](#auth-api-endpoints-documentation)
  - [Write Models / Commands](#write-models--commands)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
  - [Read Models / Queries](#read-models--queries)

## Write Models / Commands

### Register

```js
POST "/auth/register";
```

#### Register Request

```json
{
  "firstName": "Arnold",
  "lastName": "Schwarzenegger",
  "email": "aschwarzenegger@email.fi",
  "password": "123456AaBbCc?!"
}
```

#### Register Response

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

## Read Models / Queries
