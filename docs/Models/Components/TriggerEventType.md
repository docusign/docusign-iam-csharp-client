# TriggerEventType

The type of event that triggers the workflow. In this case, the workflow is initiated
by an HTTP request. Future iterations may support additional event types beyond HTTP.


## Example Usage

```csharp
using Docusign.IAM.SDK.Models.Components;

var value = TriggerEventType.Http;
```


## Values

| Name   | Value  |
| ------ | ------ |
| `Http` | HTTP   |