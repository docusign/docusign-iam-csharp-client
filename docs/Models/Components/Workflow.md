# Workflow


## Fields

| Field                                                           | Type                                                            | Required                                                        | Description                                                     |
| --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- |
| `Id`                                                            | *string*                                                        | :heavy_minus_sign:                                              | A unique ID for this workflow                                   |
| `Name`                                                          | *string*                                                        | :heavy_minus_sign:                                              | A user-provided name for this workflow                          |
| `AccountId`                                                     | *string*                                                        | :heavy_minus_sign:                                              | A unique ID for the account associated with the workflow        |
| `Status`                                                        | *string*                                                        | :heavy_minus_sign:                                              | Indicates the readiness and deployment status of a workflow     |
| `Metadata`                                                      | [ResourceMetadata](../../Models/Components/ResourceMetadata.md) | :heavy_minus_sign:                                              | N/A                                                             |