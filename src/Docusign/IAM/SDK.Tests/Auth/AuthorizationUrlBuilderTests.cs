// This file is manually maintained and should not be overwritten by code generation.
// Contains unit tests for the AuthorizationUrlBuilder utility.

using System.Web;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Utils.Auth;
using Xunit;

namespace Docusign.IAM.Tests.Utils.Auth
{
    public class AuthorizationUrlBuilderBuilderTests
    {
        private const string TEST_CLIENT_ID = "test-client-id-123";
        private const string TEST_REDIRECT_URI = "http://localhost:3000/oauth2/callback";
        private const string TEST_CODE_CHALLENGE = "zb-sqcKxz9gzaeTS3CaVWC80rE8yLA5aJVAAD1Zoek0";

        // Commonly used scopes
        private static readonly AuthScope[] TEST_SCOPES = new[]
        {
            AuthScope.Signature,
            AuthScope.Impersonation,
        };

        [Fact]
        public void DefaultConstructorSetsCorrectOAuthBasePath()
        {
            // Arrange & Act
            var builder = AuthorizationUrlBuilder.Create();

            // Build the URL to check the base path (we need to set required fields)
            builder
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI);

            string url = builder.Build();

            // Assert
            Assert.StartsWith("https://account-d.docusign.com/oauth/auth", url);
        }

        [Fact]
        public void AddScopesAllowsMultipleCallsToAddMultipleScopes()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Demo)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Add scopes in multiple calls
            builder.AddScopes(new[] { AuthScope.Signature });
            builder.AddScopes(new[] { AuthScope.Impersonation });

            // Act
            string url = builder.Build();

            // Assert
            Assert.Contains("signature", url);
            Assert.Contains("impersonation", url);
        }

        [Fact]
        public void DoesNotAddDuplicateScopes()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Demo)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Add the same scope multiple times
            builder.AddScopes(["signature"]);
            builder.AddScopes(new[] { AuthScope.Signature });

            // Act
            string url = builder.Build();

            // Assert
            Assert.Contains("scope=signature", url);

            Assert.DoesNotContain("scope=signature%20signature", url);
        }

        [Fact]
        public void BuildsValidJwtGrantAndConfidentialAuthCodeUrlForDemo()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Demo)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Act
            string url = builder.Build();

            // Assert
            Assert.StartsWith("https://account-d.docusign.com/oauth/auth", url);
            Assert.Contains("response_type=code", url);
            Assert.Contains($"client_id={TEST_CLIENT_ID}", url);
            Assert.Matches(@"scope=.*(signature)", url);
            Assert.Matches(@"scope=.*(impersonation)", url);
            Assert.Contains($"redirect_uri={HttpUtility.UrlEncode(TEST_REDIRECT_URI)}", url);
            Assert.DoesNotContain("code_challenge", url);
        }

        [Fact]
        public void BuildsValidJwtGrantAndConfidentialAuthCodeUrlForProduction()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Production)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Act
            string url = builder.Build();

            // Assert
            Assert.StartsWith("https://account.docusign.com/oauth/auth", url);
            Assert.Contains("response_type=code", url);
            Assert.Contains($"client_id={TEST_CLIENT_ID}", url);
            Assert.Matches(@"scope=.*(signature)", url);
            Assert.Matches(@"scope=.*(impersonation)", url);
            Assert.Contains($"redirect_uri={HttpUtility.UrlEncode(TEST_REDIRECT_URI)}", url);
            Assert.DoesNotContain("code_challenge", url);
        }

        [Fact]
        public void BuildsValidPublicAuthCodeUrlForDemo()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Demo)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI)
                .WithCodeChallenge(TEST_CODE_CHALLENGE);

            // Act
            string url = builder.Build();

            // Assert
            Assert.StartsWith("https://account-d.docusign.com/oauth/auth", url);
            Assert.Contains("response_type=code", url);
            Assert.Contains($"client_id={TEST_CLIENT_ID}", url);
            Assert.Contains("scope=", url);
            Assert.Contains("signature", url);
            Assert.Contains("impersonation", url);
            Assert.Contains(HttpUtility.UrlEncode(TEST_REDIRECT_URI), url);
            Assert.Contains($"code_challenge={TEST_CODE_CHALLENGE}", url);
            Assert.Contains("code_challenge_method=S256", url);
        }

        [Fact]
        public void BuildsValidPublicAuthCodeUrlForProduction()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Production)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI)
                .WithCodeChallenge(TEST_CODE_CHALLENGE);

            // Act
            string url = builder.Build();

            // Assert
            Assert.StartsWith("https://account.docusign.com/oauth/auth", url);
            Assert.Contains("response_type=code", url);
            Assert.Contains($"client_id={TEST_CLIENT_ID}", url);
            Assert.Matches(@"scope=.*(signature)", url);
            Assert.Matches(@"scope=.*(impersonation)", url);
            Assert.Contains($"redirect_uri={HttpUtility.UrlEncode(TEST_REDIRECT_URI)}", url);
            Assert.Contains($"code_challenge={TEST_CODE_CHALLENGE}", url);
            Assert.Contains("code_challenge_method=S256", url);
        }

        [Fact]
        public void BuildsValidImplicitGrantUrl()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Demo)
                .WithResponseType(AuthorizationUrlResponseType.Token)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Act
            string url = builder.Build();

            // Assert
            Assert.StartsWith("https://account-d.docusign.com/oauth/auth", url);
            Assert.Contains("response_type=token", url);
            Assert.Contains($"client_id={TEST_CLIENT_ID}", url);
            Assert.Matches(@"scope=.*(signature)", url);
            Assert.Matches(@"scope=.*(impersonation)", url);
            Assert.Contains($"redirect_uri={HttpUtility.UrlEncode(TEST_REDIRECT_URI)}", url);
        }

        [Fact]
        public void ThrowsExceptionWhenResponseTypeIsMissing()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Act - should throw
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void ThrowsExceptionWhenClientIdIsMissing()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Act - should throw
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void IncludesAllScopesWhenNoScopesAreExplicitlyAdded()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Act
            string url = builder.Build();

            // Assert
            // Get all scopes from AuthScope
            var allScopes = Enum.GetValues<AuthScope>();

            // Verify each scope is included in the URL
            foreach (var scope in allScopes)
            {
                Assert.Contains(scope.Value(), url);
            }
        }

        [Fact]
        public void ThrowsExceptionWhenRedirectUriIsMissing()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES);

            // Act - should throw
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void IncludesStateParameterWhenProvided()
        {
            // Arrange
            const string TEST_STATE = "test-state-value";
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Demo)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI)
                .WithState(TEST_STATE);

            // Act
            string url = builder.Build();

            // Assert
            Assert.Contains($"state={TEST_STATE}", url);
        }

        [Fact]
        public void DoesNotIncludeCodeChallengeMethodWithoutCodeChallenge()
        {
            // Arrange
            var builder = AuthorizationUrlBuilder
                .Create()
                .WithBasePath(OAuthBasePath.Demo)
                .WithResponseType(AuthorizationUrlResponseType.Code)
                .WithClientId(TEST_CLIENT_ID)
                .AddScopes(TEST_SCOPES)
                .WithRedirectUri(TEST_REDIRECT_URI);

            // Act
            string url = builder.Build();

            // Assert
            Assert.DoesNotContain("code_challenge_method", url);
        }
    }
}
