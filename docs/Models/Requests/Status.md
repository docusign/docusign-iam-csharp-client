# Status

Filter workflows by their status. If provided, only workflows with the specified status will be returned.
- `active`: Returns only active workflows.
- `inactive`: Returns only inactive workflows.
- `publishing`: Returns workflows currently being published.
- `unpublishing`: Returns workflows currently being unpublished.
- `archived`: Returns workflows that have been archived.
- `archiving`: Returns workflows currently being archived.        


## Example Usage

```csharp
using Docusign.IAM.SDK.Models.Requests;

var value = Status.Active;
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Active`       | active         |
| `Inactive`     | inactive       |
| `Publishing`   | publishing     |
| `Unpublishing` | unpublishing   |
| `Archived`     | archived       |
| `Archiving`    | archiving      |