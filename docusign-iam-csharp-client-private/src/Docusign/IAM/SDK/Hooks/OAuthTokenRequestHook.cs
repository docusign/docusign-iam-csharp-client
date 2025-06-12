// This file is manually maintained and should not be overwritten by code generation.
// Contains custom OAuth token request hook for handling authentication headers.

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Docusign.IAM.SDK.Models.Requests;

namespace Docusign.IAM.SDK.Hooks
{
    /// <summary>
    /// Hook that intercepts GetTokenFromRefreshToken and
    /// GetTokenFromConfidentialAuthCode requests in order to combine the clientId
    /// and secretKey into a Basic Auth header.
    /// </summary>
    public class OAuthTokenRequestHook : IBeforeRequestHook
    {
        public Task<HttpRequestMessage> BeforeRequestAsync(
            BeforeRequestContext hookCtx,
            HttpRequestMessage request
        )
        {
            switch (hookCtx.OperationID)
            {
                case "GetTokenFromRefreshToken":
                    {
                        if (hookCtx.SecuritySource == null)
                            return Task.FromResult(request);

                        var securitySource = hookCtx.SecuritySource();
                        var clientCredentials = securitySource as GetTokenFromRefreshTokenSecurity;
                        UpdateAuthorizationHeader(clientCredentials?.ClientId, clientCredentials?.SecretKey, request);
                        break;
                    }

                case "GetTokenFromConfidentialAuthCode":
                    {
                        if (hookCtx.SecuritySource == null)
                            return Task.FromResult(request);

                        var securitySource = hookCtx.SecuritySource();
                        var clientCredentials = securitySource as GetTokenFromConfidentialAuthCodeSecurity;
                        UpdateAuthorizationHeader(clientCredentials?.ClientId, clientCredentials?.SecretKey, request);
                        break;
                    }
            }

            return Task.FromResult(request);
        }

        private static void UpdateAuthorizationHeader(string? clientId, string? secretKey, HttpRequestMessage request)
        {
            string credentials = $"{clientId ?? ""}:{secretKey ?? ""}";
            string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encoded);
        }
    }
}
