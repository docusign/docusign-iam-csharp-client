# AgreementActionsChangeType

Change the agreement type to a different configured type.
Only present when the agreement type is mutable.



## Fields

| Field                                                                 | Type                                                                  | Required                                                              | Description                                                           | Example                                                               |
| --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- | --------------------------------------------------------------------- |
| `Href`                                                                | *string*                                                              | :heavy_minus_sign:                                                    | The URL for the change-type action.                                   | /v1/accounts/{accountId}/agreements/{agreementId}/actions/change-type |
| `Method`                                                              | *string*                                                              | :heavy_minus_sign:                                                    | The HTTP method for the action.                                       | PATCH                                                                 |
| `Description`                                                         | *string*                                                              | :heavy_minus_sign:                                                    | Human-readable description of the action.                             | Change the agreement type to a different configured type.             |