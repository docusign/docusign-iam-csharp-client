# Workflows
(*Maestro.Workflows*)

## Overview

### Available Operations

* [GetWorkflowsList](#getworkflowslist) - Retrieve a list of available Maestro workflows
* [GetWorkflowTriggerRequirements](#getworkflowtriggerrequirements) - Retrieve trigger requirements for a specific Maestro workflow
* [TriggerWorkflow](#triggerworkflow) - Trigger a new instance of a Maestro workflow

## GetWorkflowsList

This operation retrieves a list of all available Maestro workflows. It returns basic information
about each workflow, including its unique identifier (`id`), name, description, and the input
schema required to trigger the workflow.

The response provides key details that help users identify which workflows are available
and understand the input requirements for triggering each one. Each workflow entry also includes
metadata, such as when it was created, last modified, and by whom.

This operation is useful for obtaining an overview of all workflows within the system, helping
users and systems know what workflows are defined, what inputs they require, and how they can
be triggered.

### Use Cases:
- Listing all available workflows in a system for manual or automated workflow initiation.
- Reviewing the input requirements for a workflow before triggering it programmatically.
- Gathering basic metadata about workflows for auditing, logging, or reporting purposes.

### Key Features:
- **Comprehensive Workflow Overview**: Provides a full list of workflows, giving visibility







  into all the automated processes available within the Maestro platform.
- **Input Schema Information**: Each workflow includes its trigger input schema, showing







  what data must be provided when triggering the workflow.
- **Metadata for Tracking**: Useful metadata like creation time, last modification date,







  and user details are included to support tracking and auditing workflows.
- **Future-Proof**: The operation is designed to be expandable as more workflows are added







  or the platform grows, ensuring scalability in the workflow management process.


### Example Usage

```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Maestro.Workflows.GetWorkflowsListAsync(accountId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb");

// handle response
```

### Parameters

| Parameter                             | Type                                  | Required                              | Description                           | Example                               |
| ------------------------------------- | ------------------------------------- | ------------------------------------- | ------------------------------------- | ------------------------------------- |
| `AccountId`                           | *string*                              | :heavy_check_mark:                    | The unique identifier of the account. | ae232f1f-8efc-4b8c-bb08-626847fad8bb  |

### Response

**[WorkflowsListSuccess](../../Models/Components/WorkflowsListSuccess.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 401, 403, 404                          | application/json                            |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## GetWorkflowTriggerRequirements

This operation retrieves the configuration and input requirements necessary to trigger a specific
Maestro workflow. It provides detailed information about the `trigger_event_type`, such as HTTP
or other supported types, and specifies the required input schema, including field names, data types,
and any default values.

This information is essential for understanding the data and parameters required to initiate the
workflow. It enables developers to prepare the necessary inputs and configuration before triggering
the workflow instance, ensuring seamless execution and compliance with workflow requirements.

### Use Cases:
- Identifying the required input fields and their data types to successfully trigger the workflow.
- Reviewing the trigger configuration for validation and compliance with expected input.
- Preparing and validating data in advance of triggering the workflow, minimizing runtime errors.

### Key Features:
- **Detailed Trigger Input Requirements**: Provides an exhaustive schema of required fields,







  their data types, and optional default values for easy reference and data validation.
- **Trigger Event Type Information**: Specifies the type of event required to initiate the workflow







  (e.g., HTTP), helping developers configure their systems to invoke the workflow appropriately.
- **Configurable for Custom Triggers**: Suitable for custom configurations, enabling flexibility







  in how workflows can be triggered according to system needs.


### Example Usage

```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Maestro.Workflows.GetWorkflowTriggerRequirementsAsync(
    accountId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    workflowId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb"
);

// handle response
```

### Parameters

| Parameter                              | Type                                   | Required                               | Description                            | Example                                |
| -------------------------------------- | -------------------------------------- | -------------------------------------- | -------------------------------------- | -------------------------------------- |
| `AccountId`                            | *string*                               | :heavy_check_mark:                     | The unique identifier of the account.  | ae232f1f-8efc-4b8c-bb08-626847fad8bb   |
| `WorkflowId`                           | *string*                               | :heavy_check_mark:                     | The unique identifier of the workflow. | ae232f1f-8efc-4b8c-bb08-626847fad8bb   |

### Response

**[WorkflowTriggerRequirementsSuccess](../../Models/Components/WorkflowTriggerRequirementsSuccess.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 401, 403, 404                          | application/json                            |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |

## TriggerWorkflow

This operation triggers a new instance of a specified Maestro workflow. When invoked,
the workflow is started based on the provided input data, and the workflow instance
begins executing according to its defined logic and configuration.

The request requires an `instance_name` and any input data necessary to start the workflow,
as described by the workflow's `trigger_input_schema`. The `instance_name` is a user-defined
label for tracking the workflow run, while the input data fields should match the schema defined
in the workflow.

The operation is event-driven and typically triggered by an external HTTP event or system call,
allowing for the automatic execution of complex processes that span multiple systems or components.

Upon successful execution, the response returns the unique identifier (`id`) for the newly
created workflow instance, along with a URL (`workflow_instance_url`) that can be used to
interact with or track the running instance.

### Use Cases:
- Automating user registration workflows where input fields like `name` and `email` are provided.
- Processing financial transactions where details such as `amount` and `currency` are required.
- Sending notifications based on user interactions in other systems.

### Key Features:
- **Automated Execution**: Once triggered, the workflow runs until a step requires manual intervention.
- **Input-Driven**: Workflow execution is based on the provided input data, which is validated







  against the workflow's input schema.
- **Real-Time Triggering**: Designed to be invoked as part of an event-driven architecture,







  allowing for workflows to respond to external events.
- **Tracking and Interaction**: The response includes a URL that allows users to check the status







  of the workflow instance or take further actions while it runs.


### Example Usage

```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using System.Collections.Generic;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Maestro.Workflows.TriggerWorkflowAsync(
    accountId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    workflowId: "ae232f1f-8efc-4b8c-bb08-626847fad8bb",
    triggerWorkflow: new TriggerWorkflow() {
        InstanceName = "My Instance",
        TriggerInputs = new Dictionary<string, TriggerInputs>() {
            { "name", TriggerInputs.CreateStr(
                "Jon Doe"
            ) },
            { "email", TriggerInputs.CreateStr(
                "jdoe@example.com"
            ) },
        },
    }
);

// handle response
```

### Parameters

| Parameter                                                                                                | Type                                                                                                     | Required                                                                                                 | Description                                                                                              | Example                                                                                                  |
| -------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------- |
| `AccountId`                                                                                              | *string*                                                                                                 | :heavy_check_mark:                                                                                       | The unique identifier of the account.                                                                    | ae232f1f-8efc-4b8c-bb08-626847fad8bb                                                                     |
| `WorkflowId`                                                                                             | *string*                                                                                                 | :heavy_check_mark:                                                                                       | The unique identifier of the workflow.                                                                   | ae232f1f-8efc-4b8c-bb08-626847fad8bb                                                                     |
| `TriggerWorkflow`                                                                                        | [TriggerWorkflow](../../Models/Components/TriggerWorkflow.md)                                            | :heavy_check_mark:                                                                                       | N/A                                                                                                      | {<br/>"instance_name": "My Instance",<br/>"trigger_inputs": {<br/>"name": "Jon Doe",<br/>"email": "jdoe@example.com"<br/>}<br/>} |

### Response

**[TriggerWorkflowSuccess](../../Models/Components/TriggerWorkflowSuccess.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.Error        | 400, 401, 403, 404                          | application/json                            |
| Docusign.IAM.SDK.Models.Errors.Error        | 500                                         | application/json                            |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |