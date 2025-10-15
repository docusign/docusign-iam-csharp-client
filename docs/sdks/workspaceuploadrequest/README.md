# WorkspaceUploadRequest
(*Workspaces.WorkspaceUploadRequest*)

## Overview

### Available Operations

* [CreateWorkspaceUploadRequest](#createworkspaceuploadrequest) - Creates a new upload request within a workspace
* [GetWorkspaceUploadRequests](#getworkspaceuploadrequests) - Gets upload requests within a workspace
* [GetWorkspaceUploadRequest](#getworkspaceuploadrequest) - Gets details for a specific upload request
* [UpdateWorkspaceUploadRequest](#updateworkspaceuploadrequest) - Updates a specific upload request
* [DeleteWorkspaceUploadRequest](#deleteworkspaceuploadrequest) - Deletes a specific upload request
* [AddWorkspaceUploadRequestDocument](#addworkspaceuploadrequestdocument) - Add a document to an upload request via file upload
* [CompleteWorkspaceUploadRequest](#completeworkspaceuploadrequest) - Complete an upload request

## CreateWorkspaceUploadRequest

This operation creates a new upload request within a workspace. The upload request includes name, description, due date, and user assignments. Upload requests can be created as drafts or sent immediately based on the status field.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="createWorkspaceUploadRequest" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/upload-requests" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using System;
using System.Collections.Generic;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceUploadRequest.CreateWorkspaceUploadRequestAsync(
    accountId: "1cbbee87-a846-4a71-86d2-26b7972bb7c4",
    workspaceId: "c2ab6f98-e507-43b1-8c9d-43f1db751c40",
    createWorkspaceUploadRequestBody: new CreateWorkspaceUploadRequestBody() {
        Name = "<value>",
        Description = "what than unique limply quaintly tattered grown",
        DueDate = System.DateTime.Parse("2024-04-25T08:01:44.605Z"),
        Assignments = new List<CreateWorkspaceUploadRequestAssignment>() {
            new CreateWorkspaceUploadRequestAssignment() {
                UploadRequestResponsibilityTypeId = WorkspaceUploadRequestResponsibilityType.Assignee,
            },
        },
        Status = WorkspaceUploadRequestStatus.Draft,
    }
);

// handle response
```

### Parameters

| Parameter                                                                                       | Type                                                                                            | Required                                                                                        | Description                                                                                     |
| ----------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                     | *string*                                                                                        | :heavy_check_mark:                                                                              | The ID of the account                                                                           |
| `WorkspaceId`                                                                                   | *string*                                                                                        | :heavy_check_mark:                                                                              | The ID of the workspace                                                                         |
| `CreateWorkspaceUploadRequestBody`                                                              | [CreateWorkspaceUploadRequestBody](../../Models/Components/CreateWorkspaceUploadRequestBody.md) | :heavy_check_mark:                                                                              | The upload request details including name, description, assignments, and status                 |

### Response

**[GetWorkspaceUploadRequestResponse](../../Models/Components/GetWorkspaceUploadRequestResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkspaceUploadRequests

This operation retrieves a list of upload requests within a workspace. Each upload request includes details such as ID, name, description, status, owner information, associated documents, assignments, and various dates.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceUploadRequests" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/upload-requests" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceUploadRequest.GetWorkspaceUploadRequestsAsync(
    accountId: "5be78df1-c1f7-4e27-8b93-b0613a620dce",
    workspaceId: "b6719b00-fae9-4c7d-afce-b03f5e783434"
);

// handle response
```

### Parameters

| Parameter               | Type                    | Required                | Description             |
| ----------------------- | ----------------------- | ----------------------- | ----------------------- |
| `AccountId`             | *string*                | :heavy_check_mark:      | The ID of the account   |
| `WorkspaceId`           | *string*                | :heavy_check_mark:      | The ID of the workspace |

### Response

**[GetWorkspaceUploadRequestsResponse](../../Models/Components/GetWorkspaceUploadRequestsResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkspaceUploadRequest

This operation retrieves details about a specific upload request within a workspace. The response includes comprehensive information about the upload request including status, assigned users, associated documents, owner details, and various dates.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceUploadRequest" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/upload-requests/{uploadRequestId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceUploadRequest.GetWorkspaceUploadRequestAsync(
    accountId: "7c24b49f-1dcd-49f1-be71-5b7d65118ca4",
    workspaceId: "0d068551-32a6-491f-8107-a554d3760bc6",
    uploadRequestId: "291c9759-17f1-4e96-8db4-c006050dc1c8"
);

// handle response
```

### Parameters

| Parameter                    | Type                         | Required                     | Description                  |
| ---------------------------- | ---------------------------- | ---------------------------- | ---------------------------- |
| `AccountId`                  | *string*                     | :heavy_check_mark:           | The ID of the account        |
| `WorkspaceId`                | *string*                     | :heavy_check_mark:           | The ID of the workspace      |
| `UploadRequestId`            | *string*                     | :heavy_check_mark:           | The ID of the upload request |

### Response

**[GetWorkspaceUploadRequestResponse](../../Models/Components/GetWorkspaceUploadRequestResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## UpdateWorkspaceUploadRequest

This operation updates a specific upload request within a workspace. Only draft upload requests can be edited. The editable fields are name, description, due date, and status. Status changes are restricted - only transitions from draft to in_progress are allowed. Attempting to update a non-draft upload request will result in an INVALID_STATUS error. Attempting an invalid status change will result in an INVALID_STATUS_CHANGE error.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="updateWorkspaceUploadRequest" method="put" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/upload-requests/{uploadRequestId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceUploadRequest.UpdateWorkspaceUploadRequestAsync(
    accountId: "caf60771-df4b-4068-9ca5-0e088e4b6ebc",
    workspaceId: "da4b7335-e975-49b8-9307-923a86c3b10f",
    uploadRequestId: "5d8c2cfe-7346-46e3-a188-681b6aadfcc3",
    updateWorkspaceUploadRequestBody: new UpdateWorkspaceUploadRequestBody() {
        Name = "<value>",
        Description = "at providence phew furthermore save digitize than how circa never",
        Status = WorkspaceUploadRequestStatus.Overdue,
        DueDate = "<value>",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                       | Type                                                                                            | Required                                                                                        | Description                                                                                     |
| ----------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                     | *string*                                                                                        | :heavy_check_mark:                                                                              | The ID of the account                                                                           |
| `WorkspaceId`                                                                                   | *string*                                                                                        | :heavy_check_mark:                                                                              | The ID of the workspace                                                                         |
| `UploadRequestId`                                                                               | *string*                                                                                        | :heavy_check_mark:                                                                              | The ID of the upload request to update                                                          |
| `UpdateWorkspaceUploadRequestBody`                                                              | [UpdateWorkspaceUploadRequestBody](../../Models/Components/UpdateWorkspaceUploadRequestBody.md) | :heavy_check_mark:                                                                              | The upload request object with updated values                                                   |

### Response

**[GetWorkspaceUploadRequestResponse](../../Models/Components/GetWorkspaceUploadRequestResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## DeleteWorkspaceUploadRequest

This operation deletes a specific upload request within a workspace. Upload requests cannot be deleted if they are complete or have associated documents.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteWorkspaceUploadRequest" method="delete" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/upload-requests/{uploadRequestId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.Workspaces.WorkspaceUploadRequest.DeleteWorkspaceUploadRequestAsync(
    accountId: "4911c672-2369-401a-b334-65cc19aa9316",
    workspaceId: "1886fee0-c032-4423-a512-78c15992cb4d",
    uploadRequestId: "81d3b642-96bd-4b5e-822b-5a5ebc552ab2"
);

// handle response
```

### Parameters

| Parameter                              | Type                                   | Required                               | Description                            |
| -------------------------------------- | -------------------------------------- | -------------------------------------- | -------------------------------------- |
| `AccountId`                            | *string*                               | :heavy_check_mark:                     | The ID of the account                  |
| `WorkspaceId`                          | *string*                               | :heavy_check_mark:                     | The ID of the workspace                |
| `UploadRequestId`                      | *string*                               | :heavy_check_mark:                     | The ID of the upload request to delete |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## AddWorkspaceUploadRequestDocument

This operation adds a document to a specific upload request within a workspace via file upload. The file is passed in the request body as multipart/form-data. The file name is used as the document name.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="addWorkspaceUploadRequestDocument" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/upload-requests/{uploadRequestId}/documents" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceUploadRequest.AddWorkspaceUploadRequestDocumentAsync(
    accountId: "8b599acd-faa6-4529-b5ad-02f99b937198",
    workspaceId: "4cbc6785-7806-4970-8bca-94d8b557bc6e",
    uploadRequestId: "a1972622-e272-42d7-9477-b2574b1da2ae"
);

// handle response
```

### Parameters

| Parameter                                                                                                                         | Type                                                                                                                              | Required                                                                                                                          | Description                                                                                                                       |
| --------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                                                       | *string*                                                                                                                          | :heavy_check_mark:                                                                                                                | The ID of the account                                                                                                             |
| `WorkspaceId`                                                                                                                     | *string*                                                                                                                          | :heavy_check_mark:                                                                                                                | The ID of the workspace                                                                                                           |
| `UploadRequestId`                                                                                                                 | *string*                                                                                                                          | :heavy_check_mark:                                                                                                                | The ID of the upload request                                                                                                      |
| `AddWorkspaceUploadRequestDocumentRequest`                                                                                        | [Models.Components.AddWorkspaceUploadRequestDocumentRequest](../../Models/Components/AddWorkspaceUploadRequestDocumentRequest.md) | :heavy_minus_sign:                                                                                                                | N/A                                                                                                                               |

### Response

**[AddWorkspaceUploadRequestDocumentResponse](../../Models/Components/AddWorkspaceUploadRequestDocumentResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## CompleteWorkspaceUploadRequest

This operation completes a specific upload request within a workspace and is intended to be called by the user completing the upload request. Only upload requests that are in progress can be completed.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="completeWorkspaceUploadRequest" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/upload-requests/{uploadRequestId}/actions/complete" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.Workspaces.WorkspaceUploadRequest.CompleteWorkspaceUploadRequestAsync(
    accountId: "66e3adbf-237a-4dc6-a239-f5b562487126",
    workspaceId: "d44b9cea-0e4e-45ec-8c2f-4e0ce9729584",
    uploadRequestId: "ecdb900d-7e60-4a2c-8e83-0252dc622fcb"
);

// handle response
```

### Parameters

| Parameter                                | Type                                     | Required                                 | Description                              |
| ---------------------------------------- | ---------------------------------------- | ---------------------------------------- | ---------------------------------------- |
| `AccountId`                              | *string*                                 | :heavy_check_mark:                       | The ID of the account                    |
| `WorkspaceId`                            | *string*                                 | :heavy_check_mark:                       | The ID of the workspace                  |
| `UploadRequestId`                        | *string*                                 | :heavy_check_mark:                       | The ID of the upload request to complete |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |