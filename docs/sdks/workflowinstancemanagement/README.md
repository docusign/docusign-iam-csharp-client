# WorkflowInstanceManagement
(*Maestro.WorkflowInstanceManagement*)

## Overview

### Available Operations

* [GetWorkflowInstancesList](#getworkflowinstanceslist) - Retrieve All Workflow Instances
* [GetWorkflowInstance](#getworkflowinstance) - Retrieve a Workflow Instance
* [CancelWorkflowInstance](#cancelworkflowinstance) - Cancel a Running Workflow Instance

## GetWorkflowInstancesList

This operation retrieves a list of all available Maestro workflow instances. It returns basic information
about each workflow instance, including its unique identifier (`id`), name, status, timestamps, and
additional metadata.

The response provides key details that help users understand what workflow instances are in progress
or completed, and the relevant data for each. Each workflow instance entry also includes metadata, such
as who started it, when it was last modified, and how many steps have been completed.

### Use Cases:
- Listing all available workflow instances for manual or automated review
- Monitoring which workflow instances are currently running or have finished
- Gathering basic metadata about workflow instances for auditing, logging, or reporting purposes

### Key Features:
- **Comprehensive Instance Overview**: Provides a full list of workflow instances, giving visibility









  into all ongoing and completed workflows within the Maestro platform
- **Metadata for Tracking**: Includes helpful metadata like creation time, last modification date,









  and user details to support audit trails
- **Scalable and Future-Proof**: Designed to handle growing numbers of workflow instances as the









  platform scales


### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkflowInstancesList" method="get" path="/v1/accounts/{accountId}/workflows/{workflowId}/instances" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Maestro.WorkflowInstanceManagement.GetWorkflowInstancesListAsync(
    accountId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    workflowId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb"
);

// handle response
```

### Parameters

| Parameter                             | Type                                  | Required                              | Description                           |
| ------------------------------------- | ------------------------------------- | ------------------------------------- | ------------------------------------- |
| `AccountId`                           | *string*                              | :heavy_check_mark:                    | The unique identifier of the account. |
| `WorkflowId`                          | *string*                              | :heavy_check_mark:                    | N/A                                   |

### Response

**[WorkflowInstanceCollection](../../Models/Components/WorkflowInstanceCollection.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkflowInstance

This operation retrieves a single Maestro workflow instance by its unique identifier (`id`).
It returns the primary details of the workflow instance, including its name, status,
starting information, and other metadata.

The response provides key details that help users understand the current state of the workflow
instance, when it was started, and who initiated it. Additional metadata is included to support
auditing and reporting within the system.

### Use Cases:
- Getting the details of a specific workflow instance for further processing or review
- Monitoring the status of a running workflow instance to determine completion or cancellation
- Accessing metadata for auditing, logging, or reporting on a single workflow instance

### Key Features:
- **Single Workflow Instance**: Provides direct access to a specific workflow instance by `id`
- **Detailed Status Information**: Includes the workflow's start and end times, status, and other lifecycle timestamps
- **Metadata for Tracking**: Useful metadata like who initiated the workflow (`started_by`) and versioning details
- **Future-Proof**: Designed to be extensible if additional fields or nested information are required over time


### Example Usage

<!-- UsageSnippet language="csharp" operationID="getWorkflowInstance" method="get" path="/v1/accounts/{accountId}/workflows/{workflowId}/instances/{instanceId}" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Maestro.WorkflowInstanceManagement.GetWorkflowInstanceAsync(
    accountId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    workflowId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    instanceId: "ce20ee0f-4090-48d8-b5fa-3d05ca654f73"
);

// handle response
```

### Parameters

| Parameter                                   | Type                                        | Required                                    | Description                                 |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| `AccountId`                                 | *string*                                    | :heavy_check_mark:                          | The unique identifier of the account.       |
| `WorkflowId`                                | *string*                                    | :heavy_check_mark:                          | N/A                                         |
| `InstanceId`                                | *string*                                    | :heavy_check_mark:                          | Unique identifier for the workflow instance |

### Response

**[WorkflowInstance](../../Models/Components/WorkflowInstance.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 403, 404                               | application/json                            |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## CancelWorkflowInstance

This operation cancels a running Maestro workflow instance by its unique identifier (`instanceId`).
Once canceled, the workflow instance will no longer continue executing any remaining steps.

### Use Cases:
- Stopping a workflow execution when it is no longer needed or relevant
- Manually intervening in a workflow to prevent it from reaching completion if conditions change

### Key Features:
- **Immediate Termination**: Ensures the workflow instance no longer processes subsequent steps
- **Clear Feedback**: Returns a confirmation message including both the instance and workflow identifiers


### Example Usage

<!-- UsageSnippet language="csharp" operationID="cancelWorkflowInstance" method="post" path="/v1/accounts/{accountId}/workflows/{workflowId}/instances/{instanceId}/actions/cancel" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Maestro.WorkflowInstanceManagement.CancelWorkflowInstanceAsync(
    accountId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    workflowId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    instanceId: "ba4a94fa-3efc-4309-9463-36899a4c6d1e"
);

// handle response
```

### Parameters

| Parameter                                   | Type                                        | Required                                    | Description                                 |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| `AccountId`                                 | *string*                                    | :heavy_check_mark:                          | The unique identifier of the account.       |
| `WorkflowId`                                | *string*                                    | :heavy_check_mark:                          | N/A                                         |
| `InstanceId`                                | *string*                                    | :heavy_check_mark:                          | Unique identifier for the workflow instance |

### Response

**[CancelWorkflowInstanceResponse](../../Models/Components/CancelWorkflowInstanceResponse.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 403, 404, 409                          | application/json                            |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |