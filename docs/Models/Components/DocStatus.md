# DocStatus

Document status. Last 4 are terminal statuses. Matches enum with similar name in the backend.

## Example Usage

```csharp
using Docusign.IAM.SDK.Models.Components;

var value = DocStatus.NotStarted;
```


## Values

| Name                    | Value                   |
| ----------------------- | ----------------------- |
| `NotStarted`            | NOT_STARTED             |
| `InProgress`            | IN_PROGRESS             |
| `Canceled`              | CANCELED                |
| `Succeeded`             | SUCCEEDED               |
| `SucceededWithWarnings` | SUCCEEDED_WITH_WARNINGS |
| `Failed`                | FAILED                  |