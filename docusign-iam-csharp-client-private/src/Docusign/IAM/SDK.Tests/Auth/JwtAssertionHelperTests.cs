// This file is manually maintained and should not be overwritten by code generation.
// Contains unit tests for the JwtAssertionHelper utility.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Docusign.IAM.SDK.Models.Components;
using Docusign.IAM.SDK.Utils.Auth;
using Xunit;

namespace Docusign.IAM.SDK.Tests.Auth
{
    public class JwtAssertionHelperTests : IDisposable
    {
        private readonly RSA _rsa;
        private readonly string _privateKeyPem;
        private const string TestClientId = "test-client-id";
        private const string TestUserId = "test-user@example.com";

        public JwtAssertionHelperTests()
        {
            // Generate test RSA key pair
            _rsa = RSA.Create(2048);
            _privateKeyPem = GeneratePrivateKeyPem(_rsa);
        }

        public void Dispose()
        {
            _rsa?.Dispose();
        }

        [Fact]
        public void GenerateJWT_WithValidParameters_CreatesProperlyFormattedJWT()
        {
            // Act
            var jwt = JwtAssertionHelper.GenerateJWT(_privateKeyPem, TestClientId, TestUserId);

            // Assert
            var parts = jwt.Split('.');
            Assert.Equal(3, parts.Length);

            // Decode and verify header
            var header = DecodeJwtPart(parts[0]);
            var headerJson = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(header);

            Assert.NotNull(headerJson);
            Assert.Equal("RS256", headerJson["alg"].GetString());
            Assert.Equal("JWT", headerJson["typ"].GetString());

            // Decode and verify payload
            var payload = DecodeJwtPart(parts[1]);
            var payloadJson = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(payload);

            // Verify required claims
            Assert.NotNull(payloadJson);
            Assert.Equal(TestClientId, payloadJson["iss"].GetString());
            Assert.Equal(TestUserId, payloadJson["sub"].GetString());
            Assert.Equal("account-d.docusign.com", payloadJson["aud"].GetString());
            Assert.True(payloadJson.ContainsKey("scope"));
            Assert.True(payloadJson.ContainsKey("iat"));
            Assert.True(payloadJson.ContainsKey("exp"));
            Assert.True(payloadJson.ContainsKey("nbf"));

            // Verify time claims
            var iat = payloadJson["iat"].GetInt64();
            var exp = payloadJson["exp"].GetInt64();
            var nbf = payloadJson["nbf"].GetInt64();

            Assert.True(iat > 0);
            Assert.True(exp > 0);
            Assert.True(nbf > 0);
            Assert.Equal(3600, exp - iat); // exp should be exactly 3600 seconds (1 hour) after iat
            Assert.Equal(iat, nbf); // nbf should equal iat
        }

        [Fact]
        public void GenerateJWT_WithCustomOAuthBasePath_SetsCorrectAudience()
        {
            // Act
            var jwt = JwtAssertionHelper.GenerateJWT(
                _privateKeyPem,
                TestClientId,
                TestUserId,
                OAuthBasePath.Production
            );

            // Assert
            var parts = jwt.Split('.');
            var payload = DecodeJwtPart(parts[1]);
            var payloadJson = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(payload);

            Assert.NotNull(payloadJson);
            Assert.Equal("account.docusign.com", payloadJson["aud"].GetString());
        }

        [Fact]
        public void GenerateJWT_WithCustomScopes_SetsCorrectScopesClaim()
        {
            // Arrange
            var customScopes = new[] { "signature", "impersonation" };

            // Act
            var jwt = JwtAssertionHelper.GenerateJWT(
                _privateKeyPem,
                TestClientId,
                TestUserId,
                OAuthBasePath.Demo,
                customScopes
            );

            // Assert
            var parts = jwt.Split('.');
            var payload = DecodeJwtPart(parts[1]);
            var payloadJson = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(payload);

            Assert.NotNull(payloadJson);
            Assert.Equal("signature impersonation", payloadJson["scope"].GetString());
        }

        [Fact]
        public void GenerateJWT_WithEmptyPrivateKey_ThrowsException()
        {
            // Act & Assert
            Assert.ThrowsAny<Exception>(() =>
                JwtAssertionHelper.GenerateJWT(string.Empty, TestClientId, TestUserId)
            );
        }

        [Fact]
        public void GenerateJWT_WithInvalidPrivateKey_ThrowsException()
        {
            // Arrange
            var invalidKey = "invalid-key-string";

            // Act & Assert
            Assert.ThrowsAny<Exception>(() =>
                JwtAssertionHelper.GenerateJWT(invalidKey, TestClientId, TestUserId)
            );
        }

        [Fact]
        public void GenerateJWT_WithInvalidOAuthBasePath_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                JwtAssertionHelper.GenerateJWT(
                    _privateKeyPem,
                    TestClientId,
                    TestUserId,
                    (OAuthBasePath)999
                )
            );
        }

        [Fact]
        public void GenerateJWT_WithDefaultScopes_ContainsAllAuthScopes()
        {
            // Act
            var jwt = JwtAssertionHelper.GenerateJWT(_privateKeyPem, TestClientId, TestUserId);

            // Assert
            var parts = jwt.Split('.');
            var payload = DecodeJwtPart(parts[1]);
            var payloadJson = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(payload);

            Assert.NotNull(payloadJson);
            var scopeString = payloadJson["scope"].GetString();
            Assert.NotNull(scopeString);
            Assert.NotEmpty(scopeString);

            // Should contain default scopes from AuthScope enum
            var scopes = scopeString.Split(' ');
            Assert.True(scopes.Length > 0);
        }

        [Fact]
        public void GenerateJWT_SignatureValidation_IsValidWithPublicKey()
        {
            // Act
            var jwt = JwtAssertionHelper.GenerateJWT(_privateKeyPem, TestClientId, TestUserId);

            // Assert - Verify the JWT can be validated with the corresponding public key
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            Assert.NotNull(token);
            Assert.Equal(TestClientId, token.Issuer);
            Assert.Equal(TestUserId, token.Subject);
        }

        private static string DecodeJwtPart(string base64UrlEncoded)
        {
            // Add padding if necessary
            var padding = 4 - (base64UrlEncoded.Length % 4);
            if (padding != 4)
            {
                base64UrlEncoded += new string('=', padding);
            }

            // Convert Base64Url to Base64
            var base64 = base64UrlEncoded.Replace('-', '+').Replace('_', '/');
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

        private static string GeneratePrivateKeyPem(RSA rsa)
        {
            var privateKeyBytes = rsa.ExportRSAPrivateKey();
            var base64 = Convert.ToBase64String(privateKeyBytes);

            var pem = new StringBuilder();
            pem.AppendLine("-----BEGIN RSA PRIVATE KEY-----");

            // Split base64 into 64-character lines
            for (int i = 0; i < base64.Length; i += 64)
            {
                var lineLength = Math.Min(64, base64.Length - i);
                pem.AppendLine(base64.Substring(i, lineLength));
            }

            pem.AppendLine("-----END RSA PRIVATE KEY-----");
            return pem.ToString();
        }
    }
}

