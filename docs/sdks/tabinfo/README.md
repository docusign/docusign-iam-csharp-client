# ConnectedFields.TabInfo

## Overview

### Available Operations

* [GetConnectedFieldsTabGroups](#getconnectedfieldstabgroups) - Returns all tabs associated with the given account

## GetConnectedFieldsTabGroups

Returns all tabs associated with the given account. 

 **Note**: Unlike the Connected Fields UI, this endpoint returns only fields that are either mandatory or have the **IsRequiredForVerifyingType** <a href="https://concerto.accordproject.org/docs/design/specification/model-decorators/" target="_blank">decorator</a>

### Example Usage

<!-- UsageSnippet language="csharp" operationID="ConnectedFieldsApi_GetTabGroups" method="get" path="/v1/accounts/{accountId}/connected-fields/tab-groups" -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.ConnectedFields.TabInfo.GetConnectedFieldsTabGroupsAsync(accountId: "<id>");

// handle response
```

### Parameters

| Parameter          | Type               | Required           | Description        |
| ------------------ | ------------------ | ------------------ | ------------------ |
| `AccountId`        | *string*           | :heavy_check_mark: | N/A                |
| `AppId`            | *string*           | :heavy_minus_sign: | N/A                |

### Response

**[List<Models.Components.TabInfo>](../../Models/.md)**

### Errors

| Error Type                                  | Status Code                                 | Content Type                                |
| ------------------------------------------- | ------------------------------------------- | ------------------------------------------- |
| Docusign.IAM.SDK.Models.Errors.APIException | 4XX, 5XX                                    | \*/\*                                       |