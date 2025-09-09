# GetWorkspacesRequest


## Fields

| Field                                                                | Type                                                                 | Required                                                             | Description                                                          |
| -------------------------------------------------------------------- | -------------------------------------------------------------------- | -------------------------------------------------------------------- | -------------------------------------------------------------------- |
| `AccountId`                                                          | *string*                                                             | :heavy_check_mark:                                                   | The ID of the account                                                |
| `Count`                                                              | *int*                                                                | :heavy_minus_sign:                                                   | Number of workspaces to return. Defaults to the maximum which is 100 |
| `StartPosition`                                                      | *int*                                                                | :heavy_minus_sign:                                                   | Position of the first item in the total results. Defaults to 0       |