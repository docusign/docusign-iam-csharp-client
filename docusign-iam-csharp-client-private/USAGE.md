<!-- Start SDK Example Usage [usage] -->
```csharp
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;

var sdk = IamClient.Builder()
    .WithAccessToken("<YOUR_ACCESS_TOKEN_HERE>")
    .Build();

var res = await sdk.Auth.GetUserInfoAsync();

// handle response
```
<!-- End SDK Example Usage [usage] -->