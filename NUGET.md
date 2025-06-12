# Docusign.IAM.SDK


<!-- Start SDK Example Usage [usage] -->
## SDK Example Usage

### Example

```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Auth.GetUserInfoAsync();

// handle response
```
<!-- End SDK Example Usage [usage] -->

<!-- Start Authentication [security] -->
## Authentication

### Per-Client Security Schemes

This SDK supports the following security scheme globally:

| Name          | Type   | Scheme       |
| ------------- | ------ | ------------ |
| `AccessToken` | oauth2 | OAuth2 token |

To authenticate with the API the `AccessToken` parameter must be set when initializing the SDK client instance. For example:
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithaccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

PublicAuthCodeGrantRequestBody req = new PublicAuthCodeGrantRequestBody() {
    ClientId = "2da1cb14-xxxx-xxxx-xxxx-5b7b40829e79",
    Code = "eyJ0eXAi.....QFsje43QVZ_gw",
    CodeVerifier = "R8zFoqs0yey29G71QITZs3dK1YsdIvFNBfO4D1bukBw",
};

var res = await sdk.Auth.GetTokenFromPublicAuthCodeAsync(req);

// handle response
```

### Per-Operation Security Schemes

Some operations in this SDK require the security scheme to be specified at the request level. For example:
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
<!-- End Authentication [security] -->

<!-- Start Retries [retries] -->
## Retries

Some of the endpoints in this SDK support retries. If you use the SDK without any configuration, it will fall back to the default retry strategy provided by the API. However, the default retry strategy can be overridden on a per-operation basis, or across the entire SDK.

To change the default retry strategy for a single API call, simply pass a `RetryConfig` to the call:
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
    retryConfig: new RetryConfig(
        strategy: RetryConfig.RetryStrategy.BACKOFF,
        backoff: new BackoffStrategy(
            initialIntervalMs: 1L,
            maxIntervalMs: 50L,
            maxElapsedTimeMs: 100L,
            exponent: 1.1
        ),
        retryConnectionErrors: false
    ),
    security: new GetTokenFromConfidentialAuthCodeSecurity() {
        ClientId = "2da1cb14-xxxx-xxxx-xxxx-5b7b40829e79",
        SecretKey = "MTIzNDU2Nzxxxxxxxxxxxxxxxxxxxxx0NTY3ODkwMTI",
    },
    request: req
);

// handle response
```

If you'd like to override the default retry strategy for all operations that support retries, you can use the `RetryConfig` optional parameter when intitializing the SDK:
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithRetryConfig(retryConfig: new RetryConfig(
        strategy: RetryConfig.RetryStrategy.BACKOFF,
        backoff: new BackoffStrategy(
            initialIntervalMs: 1L,
            maxIntervalMs: 50L,
            maxElapsedTimeMs: 100L,
            exponent: 1.1
        ),
        retryConnectionErrors: false
    ))
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
<!-- End Retries [retries] -->

<!-- Start Error Handling [errors] -->
## Error Handling

Handling errors in this SDK should largely match your expectations. All operations return a response object or throw an exception.

By default, an API error will raise a `Docusign.IAM.SDK.Models.Errors.APIException` exception, which has the following properties:

| Property      | Type                  | Description           |
|---------------|-----------------------|-----------------------|
| `Message`     | *string*              | The error message     |
| `StatusCode`  | *int*                 | The HTTP status code  |
| `RawResponse` | *HttpResponseMessage* | The raw HTTP response |
| `Body`        | *string*              | The response content  |

When custom error responses are specified for an operation, the SDK may also throw their associated exceptions. You can refer to respective *Errors* tables in SDK docs for more details on possible exception types for each operation. For example, the `GetTokenFromConfidentialAuthCodeAsync` method throws the following exceptions:

| Error Type                                        | Status Code | Content Type     |
| ------------------------------------------------- | ----------- | ---------------- |
| Docusign.IAM.SDK.Models.Errors.OAuthErrorResponse | 400         | application/json |
| Docusign.IAM.SDK.Models.Errors.APIException       | 4XX, 5XX    | \*/\*            |

### Example

```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Errors;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()

    .Build();

try
{
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
}
catch (Exception ex)
{
    if (ex is OAuthErrorResponse)
    {
        // Handle exception data
        throw;
    }
    else if (ex is Docusign.IAM.SDK.Models.Errors.APIException)
    {
        // Handle default exception
        throw;
    }
}
```
<!-- End Error Handling [errors] -->

<!-- Start Server Selection [server] -->
## Server Selection

### Select Server by Name

You can override the default server globally by passing a server name to the `server: string` optional parameter when initializing the SDK client instance. The selected server will then be used as the default on the operations that use it. This table lists the names associated with the available servers:

| Name   | Server                          | Description |
| ------ | ------------------------------- | ----------- |
| `demo` | `https://api-d.docusign.com/v1` | Demo        |
| `prod` | `https://api.docusign.com/v1`   | Production  |

#### Example

```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithServer(SDKConfig.Server.Prod)
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

### Override Server URL Per-Client

The default server can also be overridden globally by passing a URL to the `serverUrl: string` optional parameter when initializing the SDK client instance. For example:
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithServerUrl("https://api-d.docusign.com/v1")
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

### Override Server URL Per-Operation

The server URL can also be overridden on a per-operation basis, provided a server list was specified for the operation. For example:
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
    request: req,
    serverUrl: "https://account.docusign.com"
);

// handle response
```
<!-- End Server Selection [server] -->

<!-- Placeholder for Future Speakeasy SDK Sections -->