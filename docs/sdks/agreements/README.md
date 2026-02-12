# Navigator.Agreements

## Overview

### Available Operations

* [GetAgreementsList](#getagreementslist) - Retrieve a list of agreements
* [GetAgreement](#getagreement) - Retrieve detailed information about a specific agreement
* [DeleteAgreement](#deleteagreement) - Delete a specific agreement

## GetAgreementsList

This operation retrieves a list of all agreements available in the system. It provides a high-level overview of each agreement, including its unique identifier (`id`), title, type, status, and involved parties. The list also includes important metadata, such as the agreement's creation and modification timestamps, and information on the agreement's source system (e.g., eSign, CLM).

Each agreement entry includes essential details that allow users to quickly assess the agreements and determine which ones are relevant for their needs. For example, the agreement's status can help users understand whether an agreement is still active, pending, or completed.

The response also includes provisions that outline the key legal, financial, and lifecycle conditions, along with custom user-defined fields, providing a comprehensive understanding of each agreement.

[Required scopes](/docs/navigator-api/auth/): `adm_store_unified_repo_read`

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

<!-- UsageSnippet language="csharp" operationID="GetAgreementsList" method="get" path="/v1/accounts/{accountId}/agreements" -->
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
    DollarFilter = "parties/name_in_agreement eq 'HEALTHEON CORPORATION'",
};

var res = await sdk.Navigator.Agreements.GetAgreementsListAsync(req);

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
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 403, 404                               | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetAgreement

This operation retrieves detailed information about a specific agreement, identified by its unique `id`. The response provides a comprehensive view of the agreement, including its title, type, status, summary, and the full list of involved parties.

In addition to general details, the operation returns provisions that define the agreement's legal, financial, lifecycle, and custom conditions. It also provides key metadata, such as creation and modification timestamps, related agreements, and user-defined or custom attributes, which help represent the structure and context of the agreement.

The operation is essential for retrieving the full context of an agreement, enabling users to understand the contract's scope, key provisions, and the legal or financial obligations that have been agreed upon.

[Required scopes](/docs/navigator-api/auth/): `adm_store_unified_repo_read`

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


### Example Usage

<!-- UsageSnippet language="csharp" operationID="GetAgreement" method="get" path="/v1/accounts/{accountId}/agreements/{agreementId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Navigator.Agreements.GetAgreementAsync(
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

### Response

**[Agreement](../../Models/Components/Agreement.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 403, 404                               | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## DeleteAgreement

This operation safely deletes an agreement. This action conforms to GDPR and CCPA compliance requirements.

[Required scopes](/docs/navigator-api/auth/): `adm_store_unified_repo_write`, `adm_store_unified_repo_read`


### Example Usage

<!-- UsageSnippet language="csharp" operationID="DeleteAgreement" method="delete" path="/v1/accounts/{accountId}/agreements/{agreementId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

await sdk.Navigator.Agreements.DeleteAgreementAsync(
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
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 403, 404                               | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/problem+json                    |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |