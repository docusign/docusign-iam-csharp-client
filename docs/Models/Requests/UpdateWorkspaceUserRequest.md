# UpdateWorkspaceUserRequest


## Fields

| Field                                                                       | Type                                                                        | Required                                                                    | Description                                                                 |
| --------------------------------------------------------------------------- | --------------------------------------------------------------------------- | --------------------------------------------------------------------------- | --------------------------------------------------------------------------- |
| `AccountId`                                                                 | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the account                                                       |
| `WorkspaceId`                                                               | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the workspace                                                     |
| `UserId`                                                                    | *string*                                                                    | :heavy_check_mark:                                                          | The ID of the user to update                                                |
| `WorkspaceUserForUpdate`                                                    | [WorkspaceUserForUpdate](../../Models/Components/WorkspaceUserForUpdate.md) | :heavy_minus_sign:                                                          | The user details to update to including the RoleId                          |