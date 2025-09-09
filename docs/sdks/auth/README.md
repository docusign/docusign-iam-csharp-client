# Auth
(*Auth*)

## Overview

### Available Operations

* [GetTokenFromConfidentialAuthCode](#gettokenfromconfidentialauthcode) - Obtains an access token from the Docusign API using an authorization code.
* [GetTokenFromPublicAuthCode](#gettokenfrompublicauthcode) - Obtains an access token from the Docusign API using an authorization code.
* [GetTokenFromJwtGrant](#gettokenfromjwtgrant) - Obtains an access token from the Docusign API using a JWT grant.
* [GetTokenFromRefreshToken](#gettokenfromrefreshtoken) - Obtains an access token from the Docusign API using an authorization code.
* [GetUserInfo](#getuserinfo) - Get user information

## GetTokenFromConfidentialAuthCode

Obtains an access token from the Docusign API using the confidential flow.
For the developer environment, the URI is https://account-d.docusign.com/oauth/token
For the production environment, the URI is https://account.docusign.com/oauth/token
You do not need an integration key to obtain an access token.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetTokenFromConfidentialAuthCode" method="post" path="/oauth/token#FromConfidentialAuthCode" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()

    .Build();

ConfidentialAuthCodeGrantRequestBody req = new ConfidentialAuthCodeGrantRequestBody() {
    Code = "eyJ0eXAi.....QFsje43QVZ_gw",
};

var res = await sdk.Auth.GetTokenFromConfidentialAuthCodeAsync(
    security: new GetTokenFromConfidentialAuthCodeSecurity() {
        ClientId = "2da1cb14-xxxx-xxxx-xxxx-5b7b40829e79",
        SecretKey = "MTIzNDU2Nzxxxxxxxxxxxxxxxxxxxxx0NTY3ODkwMTI",
    },
    request: req
);

// handle response
```

### Parameters

| Parameter                                                                                                     | Type                                                                                                          | Required                                                                                                      | Description                                                                                                   |
| ------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------- |
| `request`                                                                                                     | [ConfidentialAuthCodeGrantRequestBody](../../Models/Components/ConfidentialAuthCodeGrantRequestBody.md)       | :heavy_check_mark:                                                                                            | The request object to use for the request.                                                                    |
| `security`                                                                                                    | [GetTokenFromConfidentialAuthCodeSecurity](../../Models/Requests/GetTokenFromConfidentialAuthCodeSecurity.md) | :heavy_check_mark:                                                                                            | The security requirements to use for the request.                                                             |
| `serverURL`                                                                                                   | *string*                                                                                                      | :heavy_minus_sign:                                                                                            | An optional server URL to use.                                                                                |

### Response

**[AuthorizationCodeGrantResponse](../../Models/Components/AuthorizationCodeGrantResponse.md)**

### Errors

| Error Type                                        | Status Code                                       | Content Type                                      |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.OAuthErrorResponse | 400                                               | application/json                                  |
| Docusign.IAM.SDK.Models.Errors.APIException       | 4XX, 5XX                                          | \*/\*                                             |

## GetTokenFromPublicAuthCode

Obtains an access token from the Docusign API using the confidential flow.
For the developer environment, the URI is https://account-d.docusign.com/oauth/token
For the production environment, the URI is https://account.docusign.com/oauth/token
You do not need an integration key to obtain an access token.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetTokenFromPublicAuthCode" method="post" path="/oauth/token#FromPublicAuthCode" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

PublicAuthCodeGrantRequestBody req = new PublicAuthCodeGrantRequestBody() {
    ClientId = "2da1cb14-xxxx-xxxx-xxxx-5b7b40829e79",
    Code = "eyJ0eXAi.....QFsje43QVZ_gw",
    CodeVerifier = "R8zFoqs0yey29G71QITZs3dK1YsdIvFNBfO4D1bukBw",
};

var res = await sdk.Auth.GetTokenFromPublicAuthCodeAsync(req);

// handle response
```

### Parameters

| Parameter                                                                                   | Type                                                                                        | Required                                                                                    | Description                                                                                 |
| ------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------- |
| `request`                                                                                   | [PublicAuthCodeGrantRequestBody](../../Models/Components/PublicAuthCodeGrantRequestBody.md) | :heavy_check_mark:                                                                          | The request object to use for the request.                                                  |
| `serverURL`                                                                                 | *string*                                                                                    | :heavy_minus_sign:                                                                          | An optional server URL to use.                                                              |

### Response

**[AuthorizationCodeGrantResponse](../../Models/Components/AuthorizationCodeGrantResponse.md)**

### Errors

| Error Type                                        | Status Code                                       | Content Type                                      |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.OAuthErrorResponse | 400                                               | application/json                                  |
| Docusign.IAM.SDK.Models.Errors.APIException       | 4XX, 5XX                                          | \*/\*                                             |

## GetTokenFromJwtGrant

Obtains an access token from the Docusign API.
                                                                                                                      
For the developer environment, the URI is https://account-d.docusign.com/oauth/token
                                                                                                                      
For the production environment, the URI is https://account.docusign.com/oauth/token
                                                                                                                      
                                                                                                                      
You do not need an integration key to obtain an access token.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetTokenFromJWTGrant" method="post" path="/oauth/token#FromJWTGrant" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

JWTGrant req = new JWTGrant() {
    Assertion = "YOUR_JSON_WEB_TOKEN",
};

var res = await sdk.Auth.GetTokenFromJwtGrantAsync(req);

// handle response
```

### Parameters

| Parameter                                     | Type                                          | Required                                      | Description                                   |
| --------------------------------------------- | --------------------------------------------- | --------------------------------------------- | --------------------------------------------- |
| `request`                                     | [JWTGrant](../../Models/Requests/JWTGrant.md) | :heavy_check_mark:                            | The request object to use for the request.    |
| `serverURL`                                   | *string*                                      | :heavy_minus_sign:                            | An optional server URL to use.                |

### Response

**[JWTGrantResponse](../../Models/Components/JWTGrantResponse.md)**

### Errors

| Error Type                                        | Status Code                                       | Content Type                                      |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.OAuthErrorResponse | 400                                               | application/json                                  |
| Docusign.IAM.SDK.Models.Errors.APIException       | 4XX, 5XX                                          | \*/\*                                             |

## GetTokenFromRefreshToken

Obtains an access token from the Docusign API.
For the developer environment, the URI is https://account-d.docusign.com/oauth/token
For the production environment, the URI is https://account.docusign.com/oauth/token

You do not need an integration key to obtain an access token.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetTokenFromRefreshToken" method="post" path="/oauth/token#FromRefreshToken" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()

    .Build();

AuthorizationCodeGrant req = new AuthorizationCodeGrant() {
    RefreshToken = "<value>",
    ClientId = "2da1cb14-xxxx-xxxx-xxxx-5b7b40829e79",
};

var res = await sdk.Auth.GetTokenFromRefreshTokenAsync(
    security: new GetTokenFromRefreshTokenSecurity() {
        ClientId = "2da1cb14-xxxx-xxxx-xxxx-5b7b40829e79",
        SecretKey = "MTIzNDU2Nzxxxxxxxxxxxxxxxxxxxxx0NTY3ODkwMTI",
    },
    request: req
);

// handle response
```

### Parameters

| Parameter                                                                                     | Type                                                                                          | Required                                                                                      | Description                                                                                   |
| --------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- |
| `request`                                                                                     | [AuthorizationCodeGrant](../../Models/Requests/AuthorizationCodeGrant.md)                     | :heavy_check_mark:                                                                            | The request object to use for the request.                                                    |
| `security`                                                                                    | [GetTokenFromRefreshTokenSecurity](../../Models/Requests/GetTokenFromRefreshTokenSecurity.md) | :heavy_check_mark:                                                                            | The security requirements to use for the request.                                             |
| `serverURL`                                                                                   | *string*                                                                                      | :heavy_minus_sign:                                                                            | An optional server URL to use.                                                                |

### Response

**[GetTokenFromRefreshTokenResponse](../../Models/Requests/GetTokenFromRefreshTokenResponse.md)**

### Errors

| Error Type                                        | Status Code                                       | Content Type                                      |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.OAuthErrorResponse | 400                                               | application/json                                  |
| Docusign.IAM.SDK.Models.Errors.APIException       | 4XX, 5XX                                          | \*/\*                                             |

## GetUserInfo

This endpoint retrieves user information from the Docusign API using an access token.
For the developer environment, the URI is https://account-d.docusign.com/oauth/userinfo
For the production environment, the URI is https://account.docusign.com/oauth/userinfo

### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetUserInfo" method="get" path="/oauth/userinfo" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Auth.GetUserInfoAsync();

// handle response
```

### Parameters

| Parameter                      | Type                           | Required                       | Description                    |
| ------------------------------ | ------------------------------ | ------------------------------ | ------------------------------ |
| `serverURL`                    | *string*                       | :heavy_minus_sign:             | An optional server URL to use. |

### Response

**[UserInfo](../../Models/Components/UserInfo.md)**

### Errors

| Error Type                                        | Status Code                                       | Content Type                                      |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.OAuthErrorResponse | 400                                               | application/json                                  |
| Docusign.IAM.SDK.Models.Errors.APIException       | 4XX, 5XX                                          | \*/\*                                             |