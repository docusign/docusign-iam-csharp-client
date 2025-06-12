// This test file loads Docusign credentials and config from a .env file for local testing only.
// Make sure to add .env to your .gitignore and never check in secrets.
using System.Reflection;
using Docusign.IAM.SDK;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Models.Errors;
using Docusign.IAM.SDK.Models.Requests;
using Docusign.IAM.SDK.Utils.Auth;
using Xunit;

namespace Docusign.Tests.Integration
{
    public class DocusignClientIntegrationTests
    {
        static DocusignClientIntegrationTests()
        {
            var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyDir == null)
            {
                throw new InvalidOperationException("Could not determine assembly directory.");
            }

            var projectRootDir = Path.GetFullPath(
                Path.Combine(assemblyDir, "../../../../../../..")
            );

            var envFilePath = Path.Combine(projectRootDir, ".env");

            if (File.Exists(envFilePath))
            {
                DotNetEnv.Env.Load(envFilePath);
            }
            else
            {
                Console.WriteLine($"Warning: .env file not found at expected path: {envFilePath}");
            }
        }

        private static string JwtClientId =>
            Environment.GetEnvironmentVariable("DOCUSIGN_JWT_CLIENT_ID") ?? "";
        private static string JwtUserId =>
            Environment.GetEnvironmentVariable("DOCUSIGN_JWT_USER_ID") ?? "";
        private static string JwtPrivateKey =>
            Environment.GetEnvironmentVariable("DOCUSIGN_JWT_PRIVATE_KEY") ?? "";
        private static string ConfidentialClientId =>
            Environment.GetEnvironmentVariable("DOCUSIGN_CONFIDENTIAL_CLIENT_ID") ?? "";
        private static string ConfidentialSecretKey =>
            Environment.GetEnvironmentVariable("DOCUSIGN_CONFIDENTIAL_SECRET_KEY") ?? "";
        private static string ConfidentialAccessToken =>
            Environment.GetEnvironmentVariable("DOCUSIGN_CONFIDENTIAL_ACCESS_TOKEN") ?? "";
        private static string ConfidentialRefreshToken =>
            Environment.GetEnvironmentVariable("DOCUSIGN_CONFIDENTIAL_REFRESH_TOKEN") ?? "";
        private static int ConfidentialExpiresIn =>
            int.TryParse(
                Environment.GetEnvironmentVariable("DOCUSIGN_CONFIDENTIAL_EXPIRES_IN"),
                out var v
            )
                ? v
                : 28800;
        private static string PublicClientId =>
            Environment.GetEnvironmentVariable("DOCUSIGN_PUBLIC_CLIENT_ID") ?? "";
        private static string PublicAccessToken =>
            Environment.GetEnvironmentVariable("DOCUSIGN_PUBLIC_ACCESS_TOKEN") ?? "";
        private static string PublicRefreshToken =>
            Environment.GetEnvironmentVariable("DOCUSIGN_PUBLIC_REFRESH_TOKEN") ?? "";
        private static int PublicExpiresIn =>
            int.TryParse(
                Environment.GetEnvironmentVariable("DOCUSIGN_PUBLIC_EXPIRES_IN"),
                out var v
            )
                ? v
                : 28800;
        private static string RedirectUri =>
            Environment.GetEnvironmentVariable("DOCUSIGN_REDIRECT_URI") ?? "";
        private static OAuthBasePath OAuthBasePath =>
            Environment.GetEnvironmentVariable("DOCUSIGN_OAUTH_BASE_PATH")?.ToLower() switch
            {
                "https://account-d.docusign.com" => OAuthBasePath.Demo,
                "https://account.docusign.com" => OAuthBasePath.Production,
                _ => OAuthBasePath.Production, // Default to Production if not specified or invalid
            };
        private static string ApiBasePath =>
            Environment.GetEnvironmentVariable("DOCUSIGN_API_BASE_PATH") ?? "";
        private static string AccountId =>
            Environment.GetEnvironmentVariable("DOCUSIGN_ACCOUNT_ID") ?? "";

        [Fact]
        public async Task PublicTokenAuthFlow_EndToEnd()
        {
            Assert.False(
                string.IsNullOrEmpty(PublicClientId),
                "Missing DOCUSIGN_PUBLIC_CLIENT_ID env var."
            );
            Assert.False(
                string.IsNullOrEmpty(PublicAccessToken),
                "Missing or empty DOCUSIGN_PUBLIC_ACCESS_TOKEN env var."
            );
            Assert.False(
                string.IsNullOrEmpty(PublicRefreshToken),
                "Missing or empty DOCUSIGN_PUBLIC_REFRESH_TOKEN env var."
            );
            Assert.False(
                string.IsNullOrEmpty(OAuthBasePath.ToString()),
                "Missing DOCUSIGN_OAUTH_BASE_PATH env var."
            );
            Assert.False(
                string.IsNullOrEmpty(ApiBasePath),
                "Missing DOCUSIGN_API_BASE_PATH env var."
            );
            Assert.False(string.IsNullOrEmpty(AccountId), "Missing DOCUSIGN_ACCOUNT_ID env var.");

            // Use an unauthenticated client to get a token
            var anonClient = IamClient.Builder().WithServerUrl(ApiBasePath).Build();
            GetTokenFromRefreshTokenResponse getTokenResponse;
            getTokenResponse = await anonClient.Auth.GetTokenFromRefreshTokenAsync(
                new() { ClientId = PublicClientId, RefreshToken = PublicRefreshToken }
            );
            Assert.NotNull(getTokenResponse);

            // Use the token to create an authenticated client
            var authenticatedClient = IamClient
                .Builder()
                .WithServerUrl(ApiBasePath)
                .WithAccessToken(getTokenResponse.AccessToken)
                .Build();

            var getUserInfoResponse = await authenticatedClient.Auth.GetUserInfoAsync();
            Assert.NotNull(getUserInfoResponse);
        }

        [Fact]
        public async Task ConfidentialTokenAuthFlow_EndToEnd()
        {
            Assert.False(
                string.IsNullOrEmpty(ConfidentialClientId),
                "Missing DOCUSIGN_CONFIDENTIAL_CLIENT_ID env var."
            );
            Assert.False(
                string.IsNullOrEmpty(ConfidentialSecretKey),
                "Missing DOCUSIGN_CONFIDENTIAL_SECRET_KEY env var."
            );
            Assert.False(
                string.IsNullOrEmpty(ConfidentialAccessToken),
                "Missing or empty DOCUSIGN_CONFIDENTIAL_ACCESS_TOKEN env var."
            );
            Assert.False(
                string.IsNullOrEmpty(ConfidentialRefreshToken),
                "Missing or empty DOCUSIGN_CONFIDENTIAL_REFRESH_TOKEN env var."
            );
            Assert.False(
                string.IsNullOrEmpty(OAuthBasePath.ToString()),
                "Missing DOCUSIGN_OAUTH_BASE_PATH env var."
            );
            Assert.False(
                string.IsNullOrEmpty(ApiBasePath),
                "Missing DOCUSIGN_API_BASE_PATH env var."
            );
            Assert.False(string.IsNullOrEmpty(AccountId), "Missing DOCUSIGN_ACCOUNT_ID env var.");

            // Use an unauthenticated client to get a token
            GetTokenFromRefreshTokenResponse getTokenResponse;
            var anonClient = IamClient.Builder().WithServerUrl(ApiBasePath).Build();
            getTokenResponse = await anonClient.Auth.GetTokenFromRefreshTokenAsync(
                new() { ClientId = ConfidentialClientId, RefreshToken = ConfidentialRefreshToken },
                new() { ClientId = ConfidentialClientId, SecretKey = ConfidentialSecretKey }
            );
            Assert.NotNull(getTokenResponse);

            Assert.NotNull(getTokenResponse);

            // Use the token to create an authenticated client
            var authenticatedSdk = IamClient
                .Builder()
                .WithServerUrl(ApiBasePath)
                .WithAccessToken(getTokenResponse.AccessToken)
                .Build();

            var getUserInfoResponse = await authenticatedSdk.Auth.GetUserInfoAsync();
            Assert.NotNull(getUserInfoResponse);
        }

        [Fact]
        public async Task JWTAuthFlow_EndToEnd()
        {
            Assert.False(
                string.IsNullOrEmpty(JwtClientId),
                "Missing DOCUSIGN_JWT_CLIENT_ID env var."
            );
            Assert.False(string.IsNullOrEmpty(JwtUserId), "Missing DOCUSIGN_JWT_USER_ID env var.");
            Assert.False(
                string.IsNullOrEmpty(JwtPrivateKey),
                "Missing or empty DOCUSIGN_JWT_PRIVATE_KEY env var. Ensure it contains the actual PEM key content, not a path."
            );
            Assert.False(
                string.IsNullOrEmpty(OAuthBasePath.ToString()),
                "Missing DOCUSIGN_OAUTH_BASE_PATH env var."
            );
            Assert.False(
                string.IsNullOrEmpty(ApiBasePath),
                "Missing DOCUSIGN_API_BASE_PATH env var."
            );
            Assert.False(string.IsNullOrEmpty(AccountId), "Missing DOCUSIGN_ACCOUNT_ID env var.");

            string jwtAssertion = JwtAssertionHelper.GenerateJWT(
                clientId: JwtClientId,
                privateKey: JwtPrivateKey,
                userId: JwtUserId,
                oAuthBasePath: OAuthBasePath,
                scopes: [AuthScope.Signature.Value(), AuthScope.Impersonation.Value()]
            );

            // Use an unauthenticated client to get a token
            var anonClient = IamClient.Builder().WithServerUrl(ApiBasePath).Build();
            var tokenResponse = await anonClient.Auth.GetTokenFromJwtGrantAsync(
                new() { Assertion = jwtAssertion }
            );
            Assert.NotNull(tokenResponse);

            // Use the token to create an authenticated client
            var authenticatedSdk = IamClient
                .Builder()
                .WithServerUrl(ApiBasePath)
                .WithAccessToken(tokenResponse.AccessToken)
                .Build();

            var getUserInfo = await authenticatedSdk.Auth.GetUserInfoAsync();
            Assert.NotNull(getUserInfo);
        }
    }
}
