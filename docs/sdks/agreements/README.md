# AgreementManager.Agreements

## Overview

### Available Operations

* [GetAgreementsList](#getagreementslist) - Retrieve a list of agreements
* [PatchAgreementByDocumentId](#patchagreementbydocumentid) - Update an agreement by locating it via document ID
* [GetAgreementTypes](#getagreementtypes) - List configured agreement types for the account.
* [GetAgreement](#getagreement) - Retrieve detailed information about a specific agreement
* [DeleteAgreement](#deleteagreement) - Delete a specific agreement
* [PatchAgreement](#patchagreement) - Update specific fields of an agreement
* [ChangeAgreementType](#changeagreementtype) - Change the type of an agreement

## GetAgreementsList

This operation retrieves a list of all agreements available in the system. It provides a high-level overview of each agreement, including its unique identifier (`id`), title, type, status, and involved parties. The list also includes important metadata, such as the agreement's creation and modification timestamps, and information on the agreement's source system (e.g., eSign, CLM).

Each agreement entry includes essential details that allow users to quickly assess the agreements and determine which ones are relevant for their needs. For example, the agreement's status can help users understand whether an agreement is still active, pending, or completed.

The response also includes provisions that outline the key legal, financial, and lifecycle conditions, along with custom user-defined fields, providing a comprehensive understanding of each agreement.

### Use Cases:
- **Retrieving a list of agreements for integration into external systems**: Export or sync agreement data into other platforms (e.g., CRM, ERP systems) to align business processes across different tools.
- **Providing data for RAG (Retrieval-Augmented Generation) applications or Copilots**: The list of agreements can be a valuable data source for AI/LLM-based applications that answer user queries about agreements.
  It allows Copilots to understand what agreements exist and offer insights based on their details.
- **Filtering agreements by type or status**: Determine which agreements are active, pending, or completed, and gather a summary of key provisions across multiple agreements.
- **Auditing or reporting**: Generate a report on agreements based on type, status, or date created, helping with compliance tracking and internal reviews.
- **Metadata tracking**: Track when agreements were created, modified, and by whom, ensuring proper governance and version control.

### Key Features:
- **Comprehensive Agreement Overview**: Provides high-level visibility into all agreements, with essential details for each one, including status, type, and involved parties.
- **Metadata and Provisions**: Returns important metadata and provisions (legal, financial, and custom) for each agreement, helping users understand their obligations and contract terms.
- **Source System Information**: Captures details about where the agreement originated (e.g., eSign, CLM), making it easier to integrate and track agreements across different business systems.
- **Data for AI Applications**: The operation is designed to support LLM-powered apps, making it ideal for use in RAG-based applications and Copilots that query agreements for decision-making or information purposes.


### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetAgreementsList" method="get" path="/v1/accounts/{accountId}/agreements" example="AgreementsCollection" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Requests;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

GetAgreementsListRequest req = new GetAgreementsListRequest() {
    Limit = 10,
    Ctoken = "abc123",
    DollarSearch = "/agreements?$search=Acme",
    DollarFilter = "parties/name_in_agreement eq 'HEALTHEON CORPORATION'",
};

var res = await sdk.AgreementManager.Agreements.GetAgreementsListAsync(req);

// handle response
```

### Parameters

| Parameter                                                                     | Type                                                                          | Required                                                                      | Description                                                                   |
| ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| `request`                                                                     | [GetAgreementsListRequest](../../Models/Requests/GetAgreementsListRequest.md) | :heavy_check_mark:                                                            | The request object to use for the request.                                    |

### Response

**[AgreementsResponse](../../Models/Components/AgreementsResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404                          | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## PatchAgreementByDocumentId

This operation updates a specific agreement by first locating it using its associated `document_id`. 
This is useful when the client knows the document storage identifier but not the agreement ID.

The operation accepts a `document_id` query parameter to uniquely identify the agreement, then applies the partial updates specified in the request body. 
The system will search for an agreement containing the specified document ID, and if found, apply the requested modifications. 
The operation returns a `204 No Content` response on success.

This endpoint provides an alternative way to update agreements when the agreement ID is not readily available, making it convenient for systems that primarily work with document references rather than agreement identifiers.

### Use Cases:
- **Updating agreements referenced by document storage ID**: When integration points provide document IDs but not agreement IDs, this endpoint allows direct updates without a prior lookup.
- **Batch updating via external document references**: Systems that track agreements through external document management systems can update agreements using their document storage identifiers.
- **Cross-system synchronization**: Update agreements based on document references from external systems (e.g., content management systems, document repositories) without maintaining agreement ID mappings.
- **Document-centric workflows**: In document-first business processes, update agreements using the document reference that users are familiar with.

### Key Features:
- **Document ID Based Lookup**: Locate agreements by their associated document storage identifier rather than requiring the agreement ID upfront.
- **Automatic Resolution**: The system automatically finds the agreement associated with the provided document ID.
- **Partial Updates**: Modify only the fields you need; other agreement data remains unchanged.


### Example Usage

<!-- UsageSnippet language="csharp" operationID="PatchAgreementByDocumentId" method="patch" path="/v1/accounts/{accountId}/agreements" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using System;
using System.Collections.Generic;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.AgreementManager.Agreements.PatchAgreementByDocumentIdAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreement: new Agreement() {
        Type = "Master Service Agreement",
        Category = "BusinessServices",
        Summary = "This Master Service Agreement between Alpha Corp and Beta Ltd. defines the terms for services provided by Alpha Corp, including project scope, payment terms, and dispute resolution.",
        Provisions = null,
        Languages = new List<string?>() {
            "en-US",
        },
        SourceName = "Docusign eSign",
        SourceId = "8ade6915-d04b-40d6-bb6f-9c6ba6aa1bb5",
        SourceAccountId = "faee2c10-cae6-4d90-ba66-6d6d117d92c5",
        Metadata = new ResourceMetadata() {
            RequestId = "3f7c9e4b-851c-4f9b-89e7-123456789abc",
            ResponseTimestamp = System.DateTime.Parse("2024-10-17T14:30:00Z").ToUniversalTime(),
            ResponseDurationMs = 150,
        },
        Links = new AgreementLinks() {
            Document = new Link() {
                Href = "https://api.docusign.com/v1/accounts/12345678/agreements?limit=10&ctoken=abc123",
            },
            AgreementTypes = new AgreementLinksAgreementTypes() {
                Href = "https://api.docusign.com/v1/accounts/12345678/agreements?limit=10&ctoken=abc123",
            },
        },
        Actions = null,
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                                    | Type                                                                                                                         | Required                                                                                                                     | Description                                                                                                                  |
| ---------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                                                  | *string*                                                                                                                     | :heavy_check_mark:                                                                                                           | N/A                                                                                                                          |
| `Agreement`                                                                                                                  | [Agreement](../../Models/Components/Agreement.md)                                                                            | :heavy_check_mark:                                                                                                           | JSON payload containing the fields to be updated in the agreement.                                                           |
| `DocumentId`                                                                                                                 | *string*                                                                                                                     | :heavy_minus_sign:                                                                                                           | The unique document storage identifier associated with the agreement to be updated. This ID is used to locate the agreement. |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404                          | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetAgreementTypes

Returns the list of agreement type names configured for the specified account as a simple array of strings.

Use this endpoint to discover the available agreement types so that users can see the options to choose from. The returned type names can then be used with the change type endpoint (`PATCH /v1/accounts/{accountId}/agreements/{agreementId}/actions/change-type`) to change the type of an existing agreement.


### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetAgreementTypes" method="get" path="/v1/accounts/{accountId}/agreement-types" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementTypesAsync(accountId: "00000000-0000-0000-0000-000000000000");

// handle response
```

### Parameters

| Parameter          | Type               | Required           | Description        |
| ------------------ | ------------------ | ------------------ | ------------------ |
| `AccountId`        | *string*           | :heavy_check_mark: | N/A                |

### Response

**[List<string>](../../Models/.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404                          | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetAgreement

This operation retrieves detailed information about a specific agreement, identified by its unique `id`. The response provides a comprehensive view of the agreement, including its title, type, status, summary, and the full list of involved parties.

In addition to general details, the operation returns provisions that define the agreement's legal, financial, lifecycle, and custom conditions. It also provides key metadata, such as creation and modification timestamps, related agreements, and user-defined or custom attributes, which help represent the structure and context of the agreement.

The operation is essential for retrieving the full context of an agreement, enabling users to understand the contract's scope, key provisions, and the legal or financial obligations that have been agreed upon.

### Use Cases:
- **Integrating agreement data into external systems**: Sync detailed agreement information, such as legal and financial provisions, into external systems like ERP, CRM, or contract management tools to streamline workflows.
- **Providing detailed data for RAG (Retrieval-Augmented Generation) applications or Copilots**: Retrieve detailed agreement data for use in LLM-based applications that answer specific user queries about their agreements, such as the status of a contract, its provisions, or involved parties.
- **Retrieving the complete details of a specific agreement**: Use the full details of the agreement, including legal and financial provisions, for auditing, compliance, or review purposes.
- **Accessing agreement provisions for verification**: Verify compliance with specific legal or financial terms of the agreement, ensuring that all parties are following the agreed-upon conditions.
- **Tracking agreement changes and history**: Fetch metadata and related agreements to understand the evolution of an agreement, including modifications, associated agreements, and additional context provided by custom fields.
- **Reviewing user-defined or custom attributes**: Examine custom fields or attributes to get more context about the agreement, particularly where the business has defined custom provisions or attributes.

### Key Features:
- **Detailed Agreement Overview**: Provides a comprehensive view of a specific agreement, including its title, type, status, summary, and more.
- **Provisions for Legal, Financial, and Lifecycle Conditions**: Includes the full set of provisions that define the terms and conditions of the agreement, making it ideal for compliance and auditing purposes.
- **Metadata and History**: Tracks the agreement’s history through metadata such as creation and modification dates and user-defined fields.
- **Data Source for AI Applications**: Enables LLM-based applications to access granular agreement data, providing AI/ML-based solutions (such as Copilots) with the necessary context to answer detailed queries about an agreement.
- **Involved Parties and Related Agreements**: Lists all parties involved and related agreements, allowing users to see all associated legal documents and relationships between agreements.


### Example Usage: AddendumDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="AddendumDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: AmendmentDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="AmendmentDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: AppendixDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="AppendixDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: AttachmentDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="AttachmentDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: CertificateOfInsuranceDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="CertificateOfInsuranceDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ChangeOrderDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ChangeOrderDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ConfirmationOfRenewalDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ConfirmationOfRenewalDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ConsultingDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ConsultingDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ContractorDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ContractorDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: CreditCardDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="CreditCardDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: DistributionDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="DistributionDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: EmploymentSeparationDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="EmploymentSeparationDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: EngagementLetterDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="EngagementLetterDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: EventDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="EventDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ExhibitDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ExhibitDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: FeeDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="FeeDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: FranchiseDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="FranchiseDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: IntellectualPropertyAssignmentDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="IntellectualPropertyAssignmentDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: InvestmentAccountDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="InvestmentAccountDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: JointVentureDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="JointVentureDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: LeaseDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="LeaseDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: LetterOfIntentDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="LetterOfIntentDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: LicenseDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="LicenseDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: LoanDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="LoanDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: MarketingDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="MarketingDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: MemorandumOfUnderstandingDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="MemorandumOfUnderstandingDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: MsaDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="MsaDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: NdaDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="NdaDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: OfferLetterDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="OfferLetterDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: OrderFormDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="OrderFormDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: OtherDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="OtherDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: PartnershipDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="PartnershipDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: PrivacySecurityDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="PrivacySecurityDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ProposalDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ProposalDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: PublishingDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="PublishingDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: PurchaseDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="PurchaseDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: PurchaseOrderDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="PurchaseOrderDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: QuoteDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="QuoteDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ReleaseWaiverDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ReleaseWaiverDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: RetainerDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="RetainerDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: ServicesAgreementDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="ServicesAgreementDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: SlaDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="SlaDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: SowDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="SowDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: StockPurchaseDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="StockPurchaseDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: SubscriptionDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="SubscriptionDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: SupplementalDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="SupplementalDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: TerminationDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="TerminationDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: TermsAndConditionsDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="TermsAndConditionsDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```
### Example Usage: WealthManagementDocumentTemplate

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" example="WealthManagementDocumentTemplate" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.GetAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    includeLinkedData: false
);

// handle response
```

### Parameters

| Parameter                                                                     | Type                                                                          | Required                                                                      | Description                                                                   |
| ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| `AccountId`                                                                   | *string*                                                                      | :heavy_check_mark:                                                            | N/A                                                                           |
| `AgreementId`                                                                 | *string*                                                                      | :heavy_check_mark:                                                            | N/A                                                                           |
| `IncludeLinkedData`                                                           | *bool*                                                                        | :heavy_minus_sign:                                                            | Include linked data from external systems that correlate with this agreement. |

### Response

**[Agreement](../../Models/Components/Agreement.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404                          | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## DeleteAgreement

This operation safely deletes an agreement. This action conforms to GDPR and CCPA compliance requirements.


### Example Usage

<!-- UsageSnippet language="csharp" operationID="DeleteAgreement" method="delete" path="/v1/accounts/{accountId}/agreements/{agreementId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.AgreementManager.Agreements.DeleteAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000"
);

// handle response
```

### Parameters

| Parameter          | Type               | Required           | Description        |
| ------------------ | ------------------ | ------------------ | ------------------ |
| `AccountId`        | *string*           | :heavy_check_mark: | N/A                |
| `AgreementId`      | *string*           | :heavy_check_mark: | N/A                |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404                          | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## PatchAgreement

This operation updates one or more fields of a specific agreement identified by its `agreementId`. 
The operation supports partial updates, allowing clients to modify only the fields they need without affecting the rest of the agreement data.

The request body should contain a JSON payload with the fields to be updated. Only the fields provided in the request body will be modified; all other fields remain unchanged. The operation returns a `204 No Content` response on success.

This operation is essential for maintaining and evolving agreement data throughout the agreement's lifecycle, such as updating status, provisions, custom fields, or metadata based on business events or reviews.

### Use Cases:
- **Updating agreement status or review completion**: Mark agreements as reviewed, completed, or pending further action based on internal workflows.
- **Modifying provision details**: Update agreement terms, effective dates, expiration dates, or renewal conditions as circumstances change.
- **Managing custom attributes**: Add or modify user-defined fields and custom metadata to capture additional business context for the agreement.
- **Recording business events**: Update agreements to reflect changes such as amendments, renewals, or termination notices.
- **Synchronizing agreements across systems**: Update agreements in response to changes detected in external systems (e.g., ERP, CRM) to maintain data integrity.

### Key Features:
- **Partial Updates**: Modify only the fields you need; other agreement data remains intact.
- **Flexible Payload**: Accept JSON objects with any combination of updateable agreement fields, including provisions, metadata, and custom attributes.
- **Data Integrity**: Validates all input data to ensure compliance with agreement structure and data constraints.


### Example Usage

<!-- UsageSnippet language="csharp" operationID="PatchAgreement" method="patch" path="/v1/accounts/{accountId}/agreements/{agreementId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using System;
using System.Collections.Generic;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.AgreementManager.Agreements.PatchAgreementAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    agreement: new Agreement() {
        Type = "Master Service Agreement",
        Category = "BusinessServices",
        Summary = "This Master Service Agreement between Alpha Corp and Beta Ltd. defines the terms for services provided by Alpha Corp, including project scope, payment terms, and dispute resolution.",
        Provisions = new Provisions() {
            ConfidentialityObligationPeriod = "P30D",
            LiabilityCapDuration = "P30D",
            RenewalNoticePeriod = "P90D",
            RenewalNoticeDate = "2025-01-22T14:30:00-08:00",
            AutoRenewalTermLength = "P1Y",
            RenewalExtensionPeriod = "P1Y",
            TerminationPeriodForCause = "P6M",
            TerminationPeriodForConvenience = "P6M",
            EffectiveDate = "2025-01-22T14:30:00-08:00",
            ExpirationDate = "2025-01-22T14:30:00-08:00",
            ExecutionDate = "2025-01-22T14:30:00-08:00",
            TermLength = "P30D",
        },
        Languages = new List<string?>() {
            "en-US",
        },
        SourceName = "Docusign eSign",
        SourceId = "8ade6915-d04b-40d6-bb6f-9c6ba6aa1bb5",
        SourceAccountId = "faee2c10-cae6-4d90-ba66-6d6d117d92c5",
        Metadata = new ResourceMetadata() {
            RequestId = "3f7c9e4b-851c-4f9b-89e7-123456789abc",
            ResponseTimestamp = System.DateTime.Parse("2024-10-17T14:30:00Z").ToUniversalTime(),
            ResponseDurationMs = 150,
        },
        Links = new AgreementLinks() {
            Document = new Link() {
                Href = "https://api.docusign.com/v1/accounts/12345678/agreements?limit=10&ctoken=abc123",
            },
            AgreementTypes = new AgreementLinksAgreementTypes() {
                Href = "https://api.docusign.com/v1/accounts/12345678/agreements?limit=10&ctoken=abc123",
            },
        },
        Actions = new AgreementActions() {
            ChangeType = new AgreementActionsChangeType() {
                Href = "/v1/accounts/{accountId}/agreements/{agreementId}/actions/change-type",
                Method = "PATCH",
                Description = "Change the agreement type to a different configured type.",
            },
        },
    }
);

// handle response
```

### Parameters

| Parameter                                                          | Type                                                               | Required                                                           | Description                                                        |
| ------------------------------------------------------------------ | ------------------------------------------------------------------ | ------------------------------------------------------------------ | ------------------------------------------------------------------ |
| `AccountId`                                                        | *string*                                                           | :heavy_check_mark:                                                 | N/A                                                                |
| `AgreementId`                                                      | *string*                                                           | :heavy_check_mark:                                                 | N/A                                                                |
| `Agreement`                                                        | [Agreement](../../Models/Components/Agreement.md)                  | :heavy_check_mark:                                                 | JSON payload containing the fields to be updated in the agreement. |

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404                          | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## ChangeAgreementType

Changes the type of an existing agreement to a different configured type.
The target type must exist in the account's agreement-types collection.
Upon successful change, the server recomputes the category based on the new type.


### Example Usage

<!-- UsageSnippet language="csharp" operationID="ChangeAgreementType" method="patch" path="/v1/accounts/{accountId}/agreements/{agreementId}/actions/change-type" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.AgreementManager.Agreements.ChangeAgreementTypeAsync(
    accountId: "00000000-0000-0000-0000-000000000000",
    agreementId: "00000000-0000-0000-0000-000000000000",
    changeAgreementTypeRequest: new Docusign.IAM.SDK.Models.Components.ChangeAgreementTypeRequest() {
        Type = "MSA_DOCUMENT_DATA",
    }
);

// handle response
```

### Parameters

| Parameter                                                                                             | Type                                                                                                  | Required                                                                                              | Description                                                                                           |
| ----------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                           | *string*                                                                                              | :heavy_check_mark:                                                                                    | N/A                                                                                                   |
| `AgreementId`                                                                                         | *string*                                                                                              | :heavy_check_mark:                                                                                    | N/A                                                                                                   |
| `ChangeAgreementTypeRequest`                                                                          | [Models.Components.ChangeAgreementTypeRequest](../../Models/Components/ChangeAgreementTypeRequest.md) | :heavy_check_mark:                                                                                    | JSON payload specifying the target agreement type.                                                    |

### Response

**[ChangeAgreementTypeResponse](../../Models/Components/ChangeAgreementTypeResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 400, 401, 403, 404, 422                     | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.ErrDetails   | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |