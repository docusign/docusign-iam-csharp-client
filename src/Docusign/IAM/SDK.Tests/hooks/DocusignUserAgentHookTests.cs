using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Hooks;
using Xunit;

public class DocusignUserAgentHookTests
{
    [Fact]
    public async Task BeforeRequestAsync_ParsesUserAgentStringAndSetsNewUserAgent()
    {
        // Arrange
        var hook = new UserAgentHook();
        var request = new HttpRequestMessage();
        request.Headers.Add("User-Agent", "speakeasy-sdk/csharp 0.0.31 2.597.9 v1 docusign-test");
        var sdkConfig = new SDKConfig();
        var baseUrl = "https://api.example.com";
        var operationId = "testOperation";
        var oauth2Scopes = new System.Collections.Generic.List<string>();
        var securitySource = (System.Func<object>?)null;
        var ctx = new BeforeRequestContext(
            new HookContext(sdkConfig, baseUrl, operationId, oauth2Scopes, securitySource)
        );

        // Act
        var result = await hook.BeforeRequestAsync(ctx, request);
        var userAgent = string.Join(" ", result.Headers.GetValues("User-Agent"));

        string pattern = @"^docusign-sdk\/.+\/.+\/csharp\/.+_.+\/.+\/docusign$";
        Assert.Matches(pattern, userAgent);
    }
}
