# JWTGrant

JSON Web Token (JWT) Grant is an OAuth 2.0 flow that is used to grant an access token to service integrations


## Fields

| Field                                                                                   | Type                                                                                    | Required                                                                                | Description                                                                             | Example                                                                                 |
| --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- |
| `GrantType`                                                                             | [GetTokenFromJWTGrantGrantType](../../Models/Requests/GetTokenFromJWTGrantGrantType.md) | :heavy_minus_sign:                                                                      | The grant type.                                                                         | urn:ietf:params:oauth:grant-type:jwt-bearer                                             |
| `Assertion`                                                                             | *string*                                                                                | :heavy_check_mark:                                                                      | Your JWT                                                                                | YOUR_JSON_WEB_TOKEN                                                                     |