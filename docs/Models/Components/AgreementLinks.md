# AgreementLinks

Hypermedia controls (HATEOAS) for agreement specific links to resources.



## Fields

| Field                                                                                   | Type                                                                                    | Required                                                                                | Description                                                                             | Example                                                                                 |
| --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- |
| `Document`                                                                              | [Link](../../Models/Components/Link.md)                                                 | :heavy_minus_sign:                                                                      | A URL that references a specific resource. <br/>                                        |                                                                                         |
| `AgreementTypes`                                                                        | [AgreementLinksAgreementTypes](../../Models/Components/AgreementLinksAgreementTypes.md) | :heavy_minus_sign:                                                                      | Link to the collection of configured agreement types for the account. Always present.   | /v1/accounts/{accountId}/agreement-types                                                |