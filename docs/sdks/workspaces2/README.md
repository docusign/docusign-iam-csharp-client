# Workspaces2
(*Workspaces.Workspaces*)

## Overview

### Available Operations

* [GetWorkspaces](#getworkspaces) - Gets workspaces available to the calling user
* [CreateWorkspace](#createworkspace) - Creates a new workspace
* [GetWorkspace](#getworkspace) - Returns details about the workspace
* [GetWorkspaceAssignableRoles](#getworkspaceassignableroles) - Returns the roles the caller can assign to workspace users
* [CreateWorkspaceEnvelope](#createworkspaceenvelope) - Creates an envelope with the given documents. Returns the ID of the created envelope
* [GetWorkspaceEnvelopes](#getworkspaceenvelopes) - Returns the envelopes associated with the given workspace

## GetWorkspaces

This operation retrieves a list of workspaces available to the calling user. It returns basic information about each workspace, including its unique identifier (ID), name, and metadata such as when it was created and by whom.

Pagination is supported by passing `start_position` and `count` in the request. The response will include `resultSetSize`, `start_position`, and `end_position` which may be utilized for subsequent requests.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaces" method="get" path="/v1/accounts/{accountId}/workspaces" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.Workspaces.GetWorkspacesAsync(accountId: "c0aa779c-d467-40d4-863c-49bc82f11d0f");

// handle response
```

### Parameters

| Parameter                                                            | Type                                                                 | Required                                                             | Description                                                          |
| -------------------------------------------------------------------- | -------------------------------------------------------------------- | -------------------------------------------------------------------- | -------------------------------------------------------------------- |
| `AccountId`                                                          | *string*                                                             | :heavy_check_mark:                                                   | The ID of the account                                                |
| `Count`                                                              | *int*                                                                | :heavy_minus_sign:                                                   | Number of workspaces to return. Defaults to the maximum which is 100 |
| `StartPosition`                                                      | *int*                                                                | :heavy_minus_sign:                                                   | Position of the first item in the total results. Defaults to 0       |

### Response

**[GetWorkspacesResponse](../../Models/Components/GetWorkspacesResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## CreateWorkspace

This operation creates a new workspace. The calling user is automatically added as a member of the workspace with the role of Manage.

Once created, the `workspace_id` is utilized to associate tasks such as envelopes. Participants on tasks will automatically be added to the workspace with the Participate role.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createWorkspace" method="post" path="/v1/accounts/{accountId}/workspaces" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.Workspaces.CreateWorkspaceAsync(
    accountId: "a112e56c-a7e3-42a4-841a-04ccff785253",
    createWorkspaceBody: new CreateWorkspaceBody() {
        Name = "<value>",
    }
);

// handle response
```

### Parameters

| Parameter                                                             | Type                                                                  | Required                                                              | Description                                                           |
| --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- |
| `AccountId`                                                           | *string*                                                              | :heavy_check_mark:                                                    | The ID of the account                                                 |
| `CreateWorkspaceBody`                                                 | [CreateWorkspaceBody](../../Models/Components/CreateWorkspaceBody.md) | :heavy_check_mark:                                                    | The details of the workspace to be created including the name         |

### Response

**[CreateWorkspaceResponse](../../Models/Components/CreateWorkspaceResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkspace

This operation retrieves details about a specific workspace. It returns the workspace's unique identifier (ID), name, and metadata such as when it was created and by whom.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspace" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.Workspaces.GetWorkspaceAsync(
    accountId: "ad230bf6-0f5f-4b96-87ed-f1dfb60c2369",
    workspaceId: "55f7d452-cda5-4e74-a1a9-d0a5073bb942"
);

// handle response
```

### Parameters

| Parameter               | Type                    | Required                | Description             |
| ----------------------- | ----------------------- | ----------------------- | ----------------------- |
| `AccountId`             | *string*                | :heavy_check_mark:      | The ID of the account   |
| `WorkspaceId`           | *string*                | :heavy_check_mark:      | The ID of the workspace |

### Response

**[GetWorkspaceResponse](../../Models/Components/GetWorkspaceResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkspaceAssignableRoles

This operation returns roles that are assignable to users in the workspace based on the caller's role in the workspace. Roles available include Manage (internal) and Participate (external). Participate is the default role.

Users within the account are considered "Internal" and may be assigned any role. Users outside the account are considered "External" and may only be assigned "External" roles.

Pagination is supported by passing `start_position` and `count` in the request. The response will include `resultSetSize`, `start_position`, and `end_position` which may be utilized for subsequent requests.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceAssignableRoles" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/assignable-roles" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

GetWorkspaceAssignableRolesRequest req = new GetWorkspaceAssignableRolesRequest() {
    AccountId = "541b0318-7597-4668-b774-ac66de5ddf28",
    WorkspaceId = "62ce984d-c201-4336-9e9f-8cf191c29d9c",
};

var res = await sdk.Workspaces.Workspaces.GetWorkspaceAssignableRolesAsync(req);

// handle response
```

### Parameters

| Parameter                                                                                         | Type                                                                                              | Required                                                                                          | Description                                                                                       |
| ------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------- |
| `request`                                                                                         | [GetWorkspaceAssignableRolesRequest](../../Models/Requests/GetWorkspaceAssignableRolesRequest.md) | :heavy_check_mark:                                                                                | The request object to use for the request.                                                        |

### Response

**[GetWorkspaceAssignableRolesResponse](../../Models/Components/GetWorkspaceAssignableRolesResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## CreateWorkspaceEnvelope

This operation creates an envelope associated with the workspace. Using the `envelope_id` from the response, the [eSignature API](https://developers.docusign.com/docs/esign-rest-api/) may be utilized to modify the envelope and ultimately send it.

Envelope recipients will automatically be granted Participate access to the workspace. Envelope recipients will receive consolidated notifications from Docusign Workspaces rather than standard individual envelope notifications.

Docusign Connect events may be utilized to receive updates to individual envelope events.

The `envelopes` operation on the workspace may be utilized to query the status of all the envelopes in the workspace.

When `document_ids` is empty or excluded, the envelope is created without any documents from the workspace. eSignature API calls, including adding documents and templates, may be utilized to modify the envelope before it is sent. The eSignature API must be utilized to send the envelope.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createWorkspaceEnvelope" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/envelopes" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.Workspaces.CreateWorkspaceEnvelopeAsync(
    accountId: "d2da53cf-e564-4282-bb1d-8cdaa0948abe",
    workspaceId: "69b8ec97-5be8-40a3-ae01-fbff4ba7a447",
    workspaceEnvelopeForCreate: new WorkspaceEnvelopeForCreate() {
        EnvelopeName = "<value>",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                           | Type                                                                                                | Required                                                                                            | Description                                                                                         |
| --------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                         | *string*                                                                                            | :heavy_check_mark:                                                                                  | The ID of the account                                                                               |
| `WorkspaceId`                                                                                       | *string*                                                                                            | :heavy_check_mark:                                                                                  | The ID of the workspace                                                                             |
| `WorkspaceEnvelopeForCreate`                                                                        | [WorkspaceEnvelopeForCreate](../../Models/Components/WorkspaceEnvelopeForCreate.md)                 | :heavy_check_mark:                                                                                  | The details of the envelope to be created including the list of document IDs to add to the envelope |

### Response

**[CreateWorkspaceEnvelopeResponse](../../Models/Components/CreateWorkspaceEnvelopeResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkspaceEnvelopes

This operation retrieves a list of all associated workspace envelopes. The [`status`](https://support.docusign.com/s/document-item?bundleId=oeq1643226594604&topicId=wdm1578456348227.html) on each envelope can be used to track envelope progress. Statuses are formatted as ProperCase. e.g. `Sent`, `WaitingForOthers`, `Completed`, etc.

Based on the permissions of the caller, additional envelope details may be retrieved from the [eSignature API](https://developers.docusign.com/docs/esign-rest-api/) using the `envelope_id`.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceEnvelopes" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/envelopes" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.Workspaces.GetWorkspaceEnvelopesAsync(
    accountId: "6582b4dd-d705-43f5-8bd2-cebfd9049aa8",
    workspaceId: "c80f66a9-e39c-4ab6-818e-cf6b04f77d1a"
);

// handle response
```

### Parameters

| Parameter               | Type                    | Required                | Description             |
| ----------------------- | ----------------------- | ----------------------- | ----------------------- |
| `AccountId`             | *string*                | :heavy_check_mark:      | The ID of the account   |
| `WorkspaceId`           | *string*                | :heavy_check_mark:      | The ID of the workspace |

### Response

**[GetWorkspaceEnvelopesResponse](../../Models/Components/GetWorkspaceEnvelopesResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |