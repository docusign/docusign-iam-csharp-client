# Docusign IAM C# SDK

Developer-friendly & type-safe C# SDK specifically catered to leverage
*Docusign IAM* APIs.

## Summary

This SDK enables C# developers to abstract and simplify the
use of the Docusign IAM APIs.

By installing this nuget package, developers can then use C# objects and
methods to interact with the following Docusign APIs:

* [Maestro API](https://developers.docusign.com/docs/maestro-api/)
* [Navigator API](https://developers.docusign.com/docs/navigator-api/)
* [Connected Fields API](https://developers.docusign.com/docs/connected-fields-api/)

This repo contains the source-code for this SDK. You only need to use the code
in this repo if you want to customize the SDK for your own needs. To use the
SDK you just need to install the nuget package and do not need to use this repo.

You can also find code examples and documentation for this SDK in the [Docusign
Developer Center](https://developers.docusign.com/).

<!-- No Summary [summary] -->

<!-- Start Table of Contents [toc] -->
## Table of Contents
<!-- $toc-max-depth=2 -->
* [Docusign IAM C# SDK](#docusign-iam-c-sdk)
  * [SDK Installation](#sdk-installation)
  * [Requirements](#requirements)
  * [SDK Example Usage](#sdk-example-usage)
  * [Authentication](#authentication)
  * [Available Resources and Operations](#available-resources-and-operations)
  * [Retries](#retries)
  * [Error Handling](#error-handling)
  * [Server Selection](#server-selection)
* [Development](#development)
  * [Maturity](#maturity)
  * [Contributions](#contributions)

<!-- End Table of Contents [toc] -->

<!-- Start SDK Installation [installation] -->
## SDK Installation

### NuGet

To add the [NuGet](https://www.nuget.org/) package to a .NET project:
```bash
dotnet add package Docusign.IAM.SDK
```

### Locally

To add a reference to a local instance of the SDK in a .NET project:
```bash
dotnet add reference src/Docusign/IAM/SDK/Docusign.IAM.SDK.csproj
```
<!-- End SDK Installation [installation] -->

## Requirements

- .NET 8.0 or later
- Required NuGet packages:
  - Microsoft.IdentityModel.Tokens
  - Newtonsoft.Json
  - NodaTime
  - Portable.BouncyCastle
  - System.IdentityModel.Tokens.Jwt

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

<!-- No Authentication [security] -->

## Authentication

### Using an Access Token

Once you have obtained an access token, you can use it to make authenticated
API calls to Docusign services:

```cs
using Docusign.IAM.SDK;

// Initialize the SDK with your access token
var sdk = IamClient
    .Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

// Make API calls on behalf of the user
var res = await sdk.Auth.GetUserInfoAsync();
```

Continue reading for more information on using the SDK to obtain an access
token!

### Obtaining an Access Token

Before using the SDK to interact with Docusign APIs on behalf of a user, you
must authenticate your application. This SDK supports OAuth2 authentication
flows for secure access to Docusign services.

To get started with authentication, you will need:

* An Integrator Key (also known as "Client ID")
* A Client Secret
* Your application registered with Docusign

These credentials are obtained by registering your application in the [Docusign
Developer Portal][ds-dev-site]. This section will guide you through setting up authentication
using the confidential authorization code grant flow, which is recommended for
server-side applications.

[ds-dev-site]: https://developers.docusign.com/

#### Step 1: Obtain User Consent

Before your application can interact with the Docusign API, you must first
obtain explicit user consent. This authorization process is a mandatory first
step for all OAuth2 flows.

Construct a consent url using `AuthorizationUrlBuilder`:

```cs
  using System;
  using Docusign.IAM.SDK;
  using Docusign.IAM.SDK.Models.Components;
  using Docusign.IAM.SDK.Utils.Auth;

  // Create the authorization URL
  String consentUrl = AuthorizationUrlBuilder.Create()
      .WithBasePath(OAuthBasePath.Demo)
      .WithResponseType(AuthorizationUrlResponseType.Code)
      .WithClientId(Environment.GetEnvironmentVariable("DOCUSIGN_CLIENT_ID"))
      .WithRedirectUri(Environment.GetEnvironmentVariable("DOCUSIGN_REDIRECT_URI"))
      .Build();

  // Direct the user to grant consent
  Console.WriteLine(
      $"Please visit the following URL to authorize the application: {consentUrl}"
  );
```

After the user grants permission, Docusign will redirect them to your specified
redirect URI with an authorization code included as a query parameter named
`code`.

> [!NOTE]
> For local development, you set the redirect URI to a localhost URL (e.g.,
> `http://localhost:3000/callback`). When testing, you'll be redirected to this
> local URL and can simply copy the authorization code from your browser's
> address bar. Look for the parameter that appears after `code=` in the URL.

#### Step 2: Exchange Authorization Code for Access Token

After the user grants permission and is redirected to your application with an
authorization code, you need to exchange this code for an access token. The SDK
provides helper functions to simplify this process.

Here is an example of how one might handle the OAuth2 callback in a console
application:

```cs
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;
using Docusign.IAM.SDK.Models.Errors;
using System;
using System.Threading.Tasks;

try
{
    // Create an unauthenticated client to exchange the auth code for a token
    var anonClient = IamClient.Builder().Build();

    // Prepare the request with the auth code
    var request = new ConfidentialAuthCodeGrantRequestBody()
    {
        Code = authCode,
    };

    // Get token using client credentials
    var res = await anonClient.Auth.GetTokenFromConfidentialAuthCodeAsync(
        security: new GetTokenFromConfidentialAuthCodeSecurity()
        {
            ClientId = Environment.GetEnvironmentVariable("DOCUSIGN_CLIENT_ID"),
            SecretKey = Environment.GetEnvironmentVariable("DOCUSIGN_CLIENT_SECRET"),
        },
        request: request
    );

    /**
    * Use the token response as needed. Eg:
    * - Store the access token and refresh token in a database
    * - Use the access token to make API calls
    * - Show the user a success message
    **/

    Console.WriteLine("Access Token: " + res.AuthorizationCodeGrantResponse.AccessToken);
}
catch (OAuthErrorResponse e)
{
    // Handle OAuth error
    Console.WriteLine("OAuth Error: " + e.ErrorDescription);
}
```

> [!NOTE]
> Additional SDK methods for obtaining access can be found
> [here][gh-oauth2-docs].

[gh-oauth2-docs]: /docs/sdks/auth/README.md

#### Step 3: Use the Access Token

Once you have obtained an access token, you can use it to make authenticated
API calls to Docusign services:

```cs
using Docusign.IAM.SDK;

// Initialize the SDK with your access token
var sdk = IamClient
    .Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

// Make API calls on behalf of the user
var res = await sdk.Auth.GetUserInfoAsync();
```

<!-- Start Available Resources and Operations [operations] -->
## Available Resources and Operations

<details open>
<summary>Available methods</summary>

### [Auth](docs/sdks/auth/README.md)

* [GetTokenFromConfidentialAuthCode](docs/sdks/auth/README.md#gettokenfromconfidentialauthcode) - Obtains an access token from the Docusign API using an authorization code.
* [GetTokenFromPublicAuthCode](docs/sdks/auth/README.md#gettokenfrompublicauthcode) - Obtains an access token from the Docusign API using an authorization code.
* [GetTokenFromJwtGrant](docs/sdks/auth/README.md#gettokenfromjwtgrant) - Obtains an access token from the Docusign API using a JWT grant.
* [GetTokenFromRefreshToken](docs/sdks/auth/README.md#gettokenfromrefreshtoken) - Obtains an access token from the Docusign API using an authorization code.
* [GetUserInfo](docs/sdks/auth/README.md#getuserinfo) - Get user information

### [ConnectedFields](docs/sdks/connectedfields/README.md)


#### [ConnectedFields.TabInfo](docs/sdks/tabinfo/README.md)

* [GetConnectedFieldsTabGroups](docs/sdks/tabinfo/README.md#getconnectedfieldstabgroups) - Returns all tabs associated with the given account


### [Maestro](docs/sdks/maestro/README.md)


#### [Maestro.WorkflowInstanceManagement](docs/sdks/workflowinstancemanagement/README.md)

* [GetWorkflowInstancesList](docs/sdks/workflowinstancemanagement/README.md#getworkflowinstanceslist) - Retrieve All Workflow Instances
* [GetWorkflowInstance](docs/sdks/workflowinstancemanagement/README.md#getworkflowinstance) - Retrieve a Workflow Instance
* [CancelWorkflowInstance](docs/sdks/workflowinstancemanagement/README.md#cancelworkflowinstance) - Cancel a Running Workflow Instance

#### [Maestro.Workflows](docs/sdks/workflows/README.md)

* [GetWorkflowsList](docs/sdks/workflows/README.md#getworkflowslist) - Retrieve a list of available Maestro workflows
* [GetWorkflowTriggerRequirements](docs/sdks/workflows/README.md#getworkflowtriggerrequirements) - Retrieve trigger requirements for a specific Maestro workflow
* [TriggerWorkflow](docs/sdks/workflows/README.md#triggerworkflow) - Trigger a new instance of a Maestro workflow
* [PauseNewWorkflowInstances](docs/sdks/workflows/README.md#pausenewworkflowinstances) - Pause an Active Workflow
* [ResumePausedWorkflow](docs/sdks/workflows/README.md#resumepausedworkflow) - Resume a Paused Workflow

### [Navigator](docs/sdks/navigator/README.md)


#### [Navigator.Agreements](docs/sdks/agreements/README.md)

* [GetAgreementsList](docs/sdks/agreements/README.md#getagreementslist) - Retrieve a list of agreements
* [GetAgreement](docs/sdks/agreements/README.md#getagreement) - Retrieve detailed information about a specific agreement
* [DeleteAgreement](docs/sdks/agreements/README.md#deleteagreement) - Delete a specific agreement
* [CreateAgreementSummary](docs/sdks/agreements/README.md#createagreementsummary) - Create an AI-generated summary of an agreement document

</details>
<!-- End Available Resources and Operations [operations] -->

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

| Name   | Server                       | Description |
| ------ | ---------------------------- | ----------- |
| `demo` | `https://api-d.docusign.com` | Demo        |
| `prod` | `https://api.docusign.com`   | Production  |

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
    .WithServerUrl("https://api-d.docusign.com")
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

# Development

## Maturity

This SDK is in beta, and there may be breaking changes between versions without a major version update. Therefore, we recommend pinning usage
to a specific package version. This way, you can install the same version each time without breaking changes unless you are intentionally
looking for the latest version.

## Contributions

While we value open-source contributions to this SDK, this library is generated programmatically. Any manual changes added to internal files will be overwritten on the next generation. 
We look forward to hearing your feedback. Feel free to open a PR or an issue with a proof of concept and we'll do our best to include it in a future release. 

