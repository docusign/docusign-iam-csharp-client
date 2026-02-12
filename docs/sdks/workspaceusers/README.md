# Workspaces.WorkspaceUsers

## Overview

### Available Operations

* [GetWorkspaceUsers](#getworkspaceusers) - Retrieves the list of users in the given workspace
* [AddWorkspaceUser](#addworkspaceuser) - Adds a user to the workspace by email address
* [UpdateWorkspaceUser](#updateworkspaceuser) - Updates the specified user's role
* [RevokeWorkspaceUserAccess](#revokeworkspaceuseraccess) - Revokes the specified user's access to the workspace
* [RestoreWorkspaceUserAccess](#restoreworkspaceuseraccess) - Restores the specified user's access to the workspace

## GetWorkspaceUsers

This operations retrieves the users in a workspace. Users sent envelopes or assigned tasks will automatically be added to the workspace with the Participate role.

Pagination is supported by passing `start_position` and `count` in the request. The response will include `resultSetSize`, `start_position`, and `end_position` which may be utilized for subsequent requests.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceUsers" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/users" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

GetWorkspaceUsersRequest req = new GetWorkspaceUsersRequest() {
    AccountId = "9ae55a64-d2c4-4631-8668-7f4264e89a7c",
    WorkspaceId = "0a03290d-af53-43c0-81a3-aa5e7db57ccc",
};

var res = await sdk.Workspaces.WorkspaceUsers.GetWorkspaceUsersAsync(req);

// handle response
```

### Parameters

| Parameter                                                                     | Type                                                                          | Required                                                                      | Description                                                                   |
| ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| `request`                                                                     | [GetWorkspaceUsersRequest](../../Models/Requests/GetWorkspaceUsersRequest.md) | :heavy_check_mark:                                                            | The request object to use for the request.                                    |

### Response

**[GetWorkspaceUsersResponse](../../Models/Components/GetWorkspaceUsersResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## AddWorkspaceUser

This operation manually adds an internal or external user to a specific workspace by email address. Users within the account are considered "Internal" and may be assigned any role. Users outside the account are considered "External" and may only be assigned the Participate role. This operation is not typically needed for adding external participants to a Workspace as they will be automatically added as tasks are assigned.

Available role IDs can be retrieved via the Assignable Roles operation on a workspace. If the `role_id` is not passed, the user is added with the Participate role.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="addWorkspaceUser" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/users" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceUsers.AddWorkspaceUserAsync(
    accountId: "55ecbf41-d3bb-4ed0-bb6e-019d84813dfb",
    workspaceId: "ac4a8865-6e92-436b-8c1c-596b9bc19344"
);

// handle response
```

### Parameters

| Parameter                                                                   | Type                                                                        | Required                                                                    | Description                                                                 |
| --------------------------------------------------------------------------- | --------------------------------------------------------------------------- | --------------------------------------------------------------------------- | --------------------------------------------------------------------------- |
| `AccountId`                                                                 | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the account                                                       |
| `WorkspaceId`                                                               | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the workspace                                                     |
| `WorkspaceUserForCreate`                                                    | [WorkspaceUserForCreate](../../Models/Components/WorkspaceUserForCreate.md) | :heavy_minus_sign:                                                          | The user details                                                            |

### Response

**[CreateWorkspaceUserResponse](../../Models/Components/CreateWorkspaceUserResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## UpdateWorkspaceUser

This operation updates the specified user's role in the workspace. Users within the account are considered "Internal" and may be assigned any role. Users outside the account are considered "External" and may only be assigned "External" roles.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="updateWorkspaceUser" method="put" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/users/{userId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceUsers.UpdateWorkspaceUserAsync(
    accountId: "9c21c871-31a0-41bd-b7d0-c4bc7d7e7770",
    workspaceId: "f2cc2db5-2b59-4c1d-9b36-ec191a110bd5",
    userId: "3f0ec84d-ca81-4e4e-a476-bb1a630dde86"
);

// handle response
```

### Parameters

| Parameter                                                                   | Type                                                                        | Required                                                                    | Description                                                                 |
| --------------------------------------------------------------------------- | --------------------------------------------------------------------------- | --------------------------------------------------------------------------- | --------------------------------------------------------------------------- |
| `AccountId`                                                                 | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the account                                                       |
| `WorkspaceId`                                                               | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the workspace                                                     |
| `UserId`                                                                    | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the user to update                                                |
| `WorkspaceUserForUpdate`                                                    | [WorkspaceUserForUpdate](../../Models/Components/WorkspaceUserForUpdate.md) | :heavy_minus_sign:                                                          | The user details to update to including the RoleId                          |

### Response

**[UpdateWorkspaceUserResponse](../../Models/Components/UpdateWorkspaceUserResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## RevokeWorkspaceUserAccess

This operation revokes the specified user's access to the workspace. The optional `revocation_date` may be set to schedule revocation in the future. If not specified, the revocation will be immediate.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="revokeWorkspaceUserAccess" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/users/{userId}/actions/revoke-access" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.Workspaces.WorkspaceUsers.RevokeWorkspaceUserAccessAsync(
    accountId: "4b457d23-e0cf-41d6-ab4b-a1cc9d2746e9",
    workspaceId: "7d48c40f-5efb-4c83-8568-002406476a59",
    userId: "6307406e-ab4b-4d4b-b2c0-d2428dc6f8d4"
);

// handle response
```

### Parameters

| Parameter                                                                           | Type                                                                                | Required                                                                            | Description                                                                         |
| ----------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- |
| `AccountId`                                                                         | *string*                                                                            | :heavy_check_mark:                                                                  | The ID of the account                                                               |
| `WorkspaceId`                                                                       | *string*                                                                            | :heavy_check_mark:                                                                  | The ID of the workspace to revoke access from                                       |
| `UserId`                                                                            | *string*                                                                            | :heavy_check_mark:                                                                  | The ID of the user to be revoked from the workspace                                 |
| `RevokeWorkspaceUserDetails`                                                        | [RevokeWorkspaceUserDetails](../../Models/Components/RevokeWorkspaceUserDetails.md) | :heavy_minus_sign:                                                                  | Optional details. Allows scheduling the revocation for the future                   |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## RestoreWorkspaceUserAccess

This operation restores the specified user's access to the workspace. The user must have been previously revoked from the workspace. The access is immediately restored.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="restoreWorkspaceUserAccess" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/users/{userId}/actions/restore-access" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.Workspaces.WorkspaceUsers.RestoreWorkspaceUserAccessAsync(
    accountId: "03055c38-466e-4bf1-91d0-c49ecbc09b8f",
    workspaceId: "0c281df3-a315-4c3f-9f07-6b0a3b953797",
    userId: "cf3df2ba-fa4b-4787-b8ad-9932a4d5f94b"
);

// handle response
```

### Parameters

| Parameter                                          | Type                                               | Required                                           | Description                                        |
| -------------------------------------------------- | -------------------------------------------------- | -------------------------------------------------- | -------------------------------------------------- |
| `AccountId`                                        | *string*                                           | :heavy_check_mark:                                 | The ID of the account                              |
| `WorkspaceId`                                      | *string*                                           | :heavy_check_mark:                                 | The ID of the workspace to restore access          |
| `UserId`                                           | *string*                                           | :heavy_check_mark:                                 | The ID of the user to be restored to the workspace |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |