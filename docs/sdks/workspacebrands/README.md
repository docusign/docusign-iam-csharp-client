# WorkspaceBrands
(*Workspaces.WorkspaceBrands*)

## Overview

### Available Operations

* [GetWorkspaceBrand](#getworkspacebrand) - Returns details about the brand set for a workspace
* [UpdateWorkspaceBrand](#updateworkspacebrand) - Updates brand for an existing workspace

## GetWorkspaceBrand

This operation retrieves details about a specific workspace. It returns the brand details such as its unique identifier (ID), name, and metadata such as brand colors and logos.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceBrand" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/brand" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceBrands.GetWorkspaceBrandAsync(
    accountId: "0bfcafb4-f092-4bc8-8ef8-948bc7bf03c3",
    workspaceId: "a0cddd57-5c88-4a44-afcc-5b6de2154b65"
);

// handle response
```

### Parameters

| Parameter               | Type                    | Required                | Description             |
| ----------------------- | ----------------------- | ----------------------- | ----------------------- |
| `AccountId`             | *string*                | :heavy_check_mark:      | The ID of the account   |
| `WorkspaceId`           | *string*                | :heavy_check_mark:      | The ID of the workspace |

### Response

**[GetWorkspaceBrandResponse](../../Models/Components/GetWorkspaceBrandResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## UpdateWorkspaceBrand

This operation updates brand for a specific workspace. It returns the brand details such as its unique identifier (ID), name, and metadata such as brand colors and logos.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="updateWorkspaceBrand" method="put" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/brand" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceBrands.UpdateWorkspaceBrandAsync(
    accountId: "1b06d538-9938-4fc1-ac20-f9284b7b9a0a",
    workspaceId: "e99e34d2-4d67-46bb-89e6-29aec34fda9e",
    updateWorkspaceBrandBody: new UpdateWorkspaceBrandBody() {}
);

// handle response
```

### Parameters

| Parameter                                                                       | Type                                                                            | Required                                                                        | Description                                                                     |
| ------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| `AccountId`                                                                     | *string*                                                                        | :heavy_check_mark:                                                              | The ID of the account                                                           |
| `WorkspaceId`                                                                   | *string*                                                                        | :heavy_check_mark:                                                              | The ID of the workspace                                                         |
| `UpdateWorkspaceBrandBody`                                                      | [UpdateWorkspaceBrandBody](../../Models/Components/UpdateWorkspaceBrandBody.md) | :heavy_check_mark:                                                              | N/A                                                                             |

### Response

**[UpdateWorkspaceBrandResponse](../../Models/Components/UpdateWorkspaceBrandResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |