# PauseNewWorkflowInstancesSuccess

Indicates that a workflow has been successfully paused. New instances of this workflow will not be created.
Existing workflow instances will be unaffected.



## Fields

| Field                                                                                                                                                        | Type                                                                                                                                                         | Required                                                                                                                                                     | Description                                                                                                                                                  |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `Status`                                                                                                                                                     | *string*                                                                                                                                                     | :heavy_minus_sign:                                                                                                                                           | Represents the new state of a workflow's mechanism to permit new workflow instances from being created.<br/>Valid values include:<br/><br/><br/><br/><br/><br/><br/><br/><br/>  - active<br/>  - paused<br/> |