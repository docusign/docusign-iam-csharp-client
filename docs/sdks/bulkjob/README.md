# Navigator.BulkJob

## Overview

### Available Operations

* [CreateBulkUploadJob](#createbulkuploadjob) - Create new bulk job with presigned URLs direct to Azure Blob Store
* [GetBulkJobStatus](#getbulkjobstatus) - Get bulk job status
* [UploadCompleteBulkJob](#uploadcompletebulkjob) - Mark bulk job upload as complete

## CreateBulkUploadJob

Create a new job, give pre-signed URLs back, the client will upload to Azure Blob Store directly.

[Required scopes](/docs/navigator-api/auth/): `document_uploader_write`, `document_uploader_read`

**Important Upload Workflow**:
1. Call this endpoint to create a job and receive upload URLs
2. For each document in the response's `_embedded.documents` array, extract the `upload_document` URL from `_actions`
3. Upload your document file to each URL using an HTTP PUT request with the document content as binary data
4. After all documents are uploaded, call the `/actions/complete` endpoint to finalize the job
5. Use the GET endpoint to monitor job progress

**Example response structure**:
```json
{
  "_embedded": {
    "documents": [
      {
        "id": "8c566d26-e7fb-4b7e-870c-1d0fb8df9084",
        "sequence": 1,
        "_actions": {
          "upload_document": "https://docupstoragewestwu3dsto.blob.core.windows.net/..."
        }
      }
    ]
  }
}
```

**Azure Blob Storage Upload Instructions**:

Use the pre-signed URL from step 2 to upload your document directly to Azure Blob Storage:

```
PUT [pre-signed URL from _actions.upload_document]

Headers:
- x-ms-blob-type: BlockBlob
- x-ms-meta-filename: YourDocumentName.pdf
- Content-Type: application/pdf

Body: [Your document binary data]
```

**Important Notes**:
- The `upload_document` URLs are pre-signed Azure Blob Storage URLs with time-limited validity (8 hours)
- The `x-ms-meta-filename` header should contain your original document filename
- The `x-ms-blob-type` must be set to `BlockBlob`
- Setting the `Content-Type` header is recommended to match your document type
- If `Content-Type` is not specified, Azure defaults to `application/octet-stream`

**Firewall & Network Configuration**:

If your organization uses firewalls or network restrictions, you may need to whitelist the following Azure Blob Storage domains 
to ensure successful document uploads. The upload URLs returned by this API will use one of these domains based on your 
account's geographic region:

**Primary Storage Endpoints**:
- `https://docupstorageaustauepsto.blob.core.windows.net/`
- `https://docupstoragecanacacpsto.blob.core.windows.net/`
- `https://docupstoragecentcuspsto.blob.core.windows.net/`
- `https://docupstorageeasteu2psto.blob.core.windows.net/`
- `https://docupstorageeasteusdsto.blob.core.windows.net/` (Demo)
- `https://docupstoragejapajpepsto.blob.core.windows.net/`
- `https://docupstoragenortneupsto.blob.core.windows.net/`
- `https://docupstoragewestweupsto.blob.core.windows.net/`
- `https://docupstoragewestwu3dsto.blob.core.windows.net/` (Demo)

**Secondary Storage Endpoints** (for redundancy/failover):
- `https://docupstorageaustauepsto-secondary.blob.core.windows.net/`
- `https://docupstoragecanacacpsto-secondary.blob.core.windows.net/`
- `https://docupstoragecentcuspsto-secondary.blob.core.windows.net/`
- `https://docupstorageeasteu2psto-secondary.blob.core.windows.net/`
- `https://docupstorageeasteusdsto-secondary.blob.core.windows.net/` (Demo)
- `https://docupstoragejapajpepsto-secondary.blob.core.windows.net/`
- `https://docupstoragenortneupsto-secondary.blob.core.windows.net/`
- `https://docupstoragewestweupsto-secondary.blob.core.windows.net/`
- `https://docupstoragewestwu3dsto-secondary.blob.core.windows.net/` (Demo)

**Note**: You may whitelist all domains listed above, or contact your DocuSign administrator to determine which specific 
region(s) your account uses to minimize the whitelist scope.

**Supported File Formats & Content Types**:

The table below shows common file formats and their recommended Content-Type headers. 
**Note**: For the most up-to-date list of supported formats, headers, and constraints, always refer to the 
`_action_templates` object in the API response, which provides dynamic configuration including:
- `allowed_formats`: Current list of supported file extensions
- `headers`: Required HTTP headers with examples
- `constraints`: Maximum file size and other limits
- `success_status_code`: Expected response code for successful uploads

| Format | Extension | Content-Type |
|--------|-----------|--------------|
| PDF | .pdf | `application/pdf` |
| Word Document (2007+) | .docx | `application/vnd.openxmlformats-officedocument.wordprocessingml.document` |
| Word Document (Legacy) | .doc | `application/msword` |
| PowerPoint Presentation (2007+) | .pptx | `application/vnd.openxmlformats-officedocument.presentationml.presentation` |
| PowerPoint Presentation (Legacy) | .ppt | `application/vnd.ms-powerpoint` |
| PowerPoint Slideshow | .ppsx | `application/vnd.openxmlformats-officedocument.presentationml.slideshow` |
| Excel Workbook (2007+) | .xlsx | `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet` |
| Excel Workbook (Legacy) | .xls | `application/vnd.ms-excel` |
| Excel Binary Workbook | .xlsb | `application/vnd.ms-excel.sheet.binary.macroenabled.12` |
| Rich Text Format | .rtf | `text/rtf` |
| WordPerfect Document | .wpd | `application/vnd.wordperfect` |
| HTML | .html, .htm | `text/html` |
| JPEG Image | .jpg, .jpeg | `image/jpeg` |
| PNG Image | .png | `image/png` |
| TIFF Image | .tif, .tiff | `image/tiff` |

**Example Upload Requests**:

PDF Document:
```
PUT https://storage.blob.core.windows.net/container/doc-id?signature=...
Content-Type: application/pdf
x-ms-blob-type: BlockBlob
x-ms-meta-filename: contract.pdf

[Binary PDF data]
```

Word Document:
```
PUT https://storage.blob.core.windows.net/container/doc-id?signature=...
Content-Type: application/vnd.openxmlformats-officedocument.wordprocessingml.document
x-ms-blob-type: BlockBlob
x-ms-meta-filename: agreement.docx

[Binary DOCX data]
```

Image:
```
PUT https://storage.blob.core.windows.net/container/doc-id?signature=...
Content-Type: image/jpeg
x-ms-blob-type: BlockBlob
x-ms-meta-filename: signed-page.jpg

[Binary JPEG data]
```


### Example Usage

<!-- UsageSnippet language="csharp" operationID="createBulkUploadJob" method="post" path="/v1/accounts/{accountId}/upload/jobs" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Navigator.BulkJob.CreateBulkUploadJobAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    createBulkJob: new CreateBulkJob() {
        ExpectedNumberOfDocs = 2,
        JobName = "Q4 2025 Contracts",
        Language = "en-US",
    }
);

// handle response
```

### Parameters

| Parameter                                                 | Type                                                      | Required                                                  | Description                                               |
| --------------------------------------------------------- | --------------------------------------------------------- | --------------------------------------------------------- | --------------------------------------------------------- |
| `AccountId`                                               | *string*                                                  | :heavy_check_mark:                                        | N/A                                                       |
| `CreateBulkJob`                                           | [CreateBulkJob](../../Models/Components/CreateBulkJob.md) | :heavy_check_mark:                                        | N/A                                                       |

### Response

**[Models.Components.BulkJob](../../Models/Components/BulkJob.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 429                          | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetBulkJobStatus

Get the current status and details of a bulk job.

[Required scopes](/docs/navigator-api/auth/): `document_uploader_read`

### Example Usage

<!-- UsageSnippet language="csharp" operationID="getBulkJobStatus" method="get" path="/v1/accounts/{accountId}/upload/jobs/{jobId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Navigator.BulkJob.GetBulkJobStatusAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    jobId: "00000000-0000-0000-0000-000000000000"
);

// handle response
```

### Parameters

| Parameter          | Type               | Required           | Description        |
| ------------------ | ------------------ | ------------------ | ------------------ |
| `AccountId`        | *string*           | :heavy_check_mark: | N/A                |
| `JobId`            | *string*           | :heavy_check_mark: | N/A                |

### Response

**[Models.Components.BulkJob](../../Models/Components/BulkJob.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 401, 403, 404                               | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## UploadCompleteBulkJob

Mark the upload of documents as complete for a bulk job. 
End user won't upload more docs for this job.

**Important**: Only call this endpoint after successfully uploading all documents to their respective pre-signed URLs obtained from the create job response.

[Required scopes](/docs/navigator-api/auth/): `document_uploader_write`, `document_uploader_read`


### Example Usage

<!-- UsageSnippet language="csharp" operationID="uploadCompleteBulkJob" method="post" path="/v1/accounts/{accountId}/upload/jobs/{jobId}/actions/complete" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Navigator.BulkJob.UploadCompleteBulkJobAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    jobId: "00000000-0000-0000-0000-000000000000"
);

// handle response
```

### Parameters

| Parameter          | Type               | Required           | Description        |
| ------------------ | ------------------ | ------------------ | ------------------ |
| `AccountId`        | *string*           | :heavy_check_mark: | N/A                |
| `JobId`            | *string*           | :heavy_check_mark: | N/A                |

### Response

**[Models.Components.BulkJob](../../Models/Components/BulkJob.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404                          | application/json                            |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |