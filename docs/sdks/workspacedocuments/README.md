# Workspaces.WorkspaceDocuments

## Overview

### Available Operations

* [GetWorkspaceDocuments](#getworkspacedocuments) - Get documents in the workspace accessible to the calling user
* [AddWorkspaceDocument](#addworkspacedocument) - Add a document to a workspace via file contents upload
* [GetWorkspaceDocument](#getworkspacedocument) - Get information about the document
* [DeleteWorkspaceDocument](#deleteworkspacedocument) - Deletes a document in the workspace
* [GetWorkspaceDocumentContents](#getworkspacedocumentcontents) - Get the file contents of the document

## GetWorkspaceDocuments

This operation retrieves the documents in the workspace that are accessible to the calling user. Documents may be added directly or automatically through tasks such as envelopes. Documents may be used to create envelopes.

Pagination is supported by passing `start_position` and `count` in the request. The response will include `resultSetSize`, `start_position`, and `end_position` which may be utilized for subsequent requests.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceDocuments" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/documents" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

GetWorkspaceDocumentsRequest req = new GetWorkspaceDocumentsRequest() {
    AccountId = "61364114-072d-477f-a9fc-f9af7aea7896",
    WorkspaceId = "d44e8655-55a3-498e-bfc3-e23027c5c36a",
};

var res = await sdk.Workspaces.WorkspaceDocuments.GetWorkspaceDocumentsAsync(req);

// handle response
```

### Parameters

| Parameter                                                                             | Type                                                                                  | Required                                                                              | Description                                                                           |
| ------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------- |
| `request`                                                                             | [GetWorkspaceDocumentsRequest](../../Models/Requests/GetWorkspaceDocumentsRequest.md) | :heavy_check_mark:                                                                    | The request object to use for the request.                                            |

### Response

**[GetWorkspaceDocumentsResponse](../../Models/Components/GetWorkspaceDocumentsResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## AddWorkspaceDocument

This operation adds a document to a workspace via file contents upload. The file is passed in the request body as a multipart/form-data file. The file name is used as the document name.

Once added, it may be used to create an envelope associated with the workspace.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="addWorkspaceDocument" method="post" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/documents" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceDocuments.AddWorkspaceDocumentAsync(
    accountId: "5eddb8e1-d00e-47c4-9ed6-3b1c8915ae0d",
    workspaceId: "7f9e0991-b6d1-4de8-bfa5-7724e59a3087"
);

// handle response
```

### Parameters

| Parameter                                                                                               | Type                                                                                                    | Required                                                                                                | Description                                                                                             |
| ------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                             | *string*                                                                                                | :heavy_check_mark:                                                                                      | The ID of the account                                                                                   |
| `WorkspaceId`                                                                                           | *string*                                                                                                | :heavy_check_mark:                                                                                      | The ID of the workspace                                                                                 |
| `AddWorkspaceDocumentRequest`                                                                           | [Models.Components.AddWorkspaceDocumentRequest](../../Models/Components/AddWorkspaceDocumentRequest.md) | :heavy_minus_sign:                                                                                      | N/A                                                                                                     |

### Response

**[CreateWorkspaceDocumentResponse](../../Models/Components/CreateWorkspaceDocumentResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkspaceDocument

This operation retrieves information about the document. The response includes the document ID, name, and metadata.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceDocument" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/documents/{documentId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceDocuments.GetWorkspaceDocumentAsync(
    accountId: "92293164-1793-41a1-8cb1-d6fdf0660804",
    workspaceId: "dfef9b70-860f-4798-889d-2f28cf5df5f4",
    documentId: "b9ed137b-5b0a-4abf-abac-ab9720001190"
);

// handle response
```

### Parameters

| Parameter               | Type                    | Required                | Description             |
| ----------------------- | ----------------------- | ----------------------- | ----------------------- |
| `AccountId`             | *string*                | :heavy_check_mark:      | The ID of the account   |
| `WorkspaceId`           | *string*                | :heavy_check_mark:      | The ID of the workspace |
| `DocumentId`            | *string*                | :heavy_check_mark:      | The ID of the document  |

### Response

**[GetWorkspaceDocumentResponse](../../Models/Components/GetWorkspaceDocumentResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## DeleteWorkspaceDocument

This operation permanently deletes a document by ID.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="deleteWorkspaceDocument" method="delete" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/documents/{documentId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.Workspaces.WorkspaceDocuments.DeleteWorkspaceDocumentAsync(
    accountId: "2e37a9af-e272-4059-96ff-0bfcf9620437",
    workspaceId: "0013f129-d585-40d0-a299-1141daa04cf3",
    documentId: "20dad844-6281-4b04-834a-b5979c0329b7"
);

// handle response
```

### Parameters

| Parameter               | Type                    | Required                | Description             |
| ----------------------- | ----------------------- | ----------------------- | ----------------------- |
| `AccountId`             | *string*                | :heavy_check_mark:      | The ID of the account   |
| `WorkspaceId`           | *string*                | :heavy_check_mark:      | The ID of the workspace |
| `DocumentId`            | *string*                | :heavy_check_mark:      | The ID of the document  |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkspaceDocumentContents

This operation retrieves the file contents of the document. The file is returned as a stream in the response body. The Content-Disposition response header contains the document name as the `filename`.

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkspaceDocumentContents" method="get" path="/v1/accounts/{accountId}/workspaces/{workspaceId}/documents/{documentId}/contents" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Workspaces.WorkspaceDocuments.GetWorkspaceDocumentContentsAsync(
    accountId: "4bc13f41-0697-41ee-8a11-d96266a80841",
    workspaceId: "4a268145-6144-48d9-b009-283af8fd83e8",
    documentId: "b62fd488-5ecf-4b73-878f-72550a413ac3"
);

// handle response
```

### Parameters

| Parameter               | Type                    | Required                | Description             |
| ----------------------- | ----------------------- | ----------------------- | ----------------------- |
| `AccountId`             | *string*                | :heavy_check_mark:      | The ID of the account   |
| `WorkspaceId`           | *string*                | :heavy_check_mark:      | The ID of the workspace |
| `DocumentId`            | *string*                | :heavy_check_mark:      | The ID of the document  |

### Response

**[byte[]](../../Models/.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 400, 401                                    | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrorDetails | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |