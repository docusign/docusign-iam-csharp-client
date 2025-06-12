// This file is manually maintained and should not be overwritten by code generation.
// Contains custom utilities for building Docusign authorization URLs.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Docusign.IAM.SDK.Models.Components;

namespace Docusign.IAM.SDK.Utils.Auth
{
    /// <summary>
    /// Builder for creating Docusign authorization URLs.
    /// </summary>
    public class AuthorizationUrlBuilder
    {
        private HashSet<string>? _scopes;
        private String _oauthBasePath;
        private String? _responseType;
        private String? _clientId;
        private String? _redirectUri;
        private String? _state;
        private String? _codeChallenge;

        private AuthorizationUrlBuilder(string oauthBasePath)
        {
            _oauthBasePath = oauthBasePath;
        }

        /// <summary>
        /// Creates a new instance of the AuthorizationUrlBuilder builder.
        ///
        /// This method initializes the builder with the default OAuth base path (Demo).
        /// </summary>
        public static AuthorizationUrlBuilder Create()
        {
            return new AuthorizationUrlBuilder(OAuthBasePath.Demo.Value());
        }

        /// <summary>
        /// Sets the environment for the Docusign instance.
        /// </summary>
        /// <param name="oauthBasePath">Must be Demo or Production.</param>
        /// <returns>The builder instance for method chaining.</returns>
        /// <exception cref="ArgumentException">Thrown when the environment is invalid.</exception>
        public AuthorizationUrlBuilder WithBasePath(OAuthBasePath oauthBasePath)
        {
            _oauthBasePath = oauthBasePath.Value();
            return this;
        }

        /// <summary>
        /// Sets the response type for the OAuth flow.
        /// </summary>
        /// <param name="responseType">The OAuth response type.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public AuthorizationUrlBuilder WithResponseType(AuthorizationUrlResponseType responseType)
        {
            _responseType = responseType.ToEnumMemberValue();
            return this;
        }

        /// <summary>
        /// Sets the client ID for the Docusign application.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public AuthorizationUrlBuilder WithClientId(string clientId)
        {
            _clientId = clientId;
            return this;
        }

        /// <summary>
        /// Sets the scopes requested for the OAuth token.
        /// </summary>
        /// <param name="scopes">The requested scopes.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public AuthorizationUrlBuilder AddScopes(IEnumerable<AuthScope> scopes)
        {
            return AddScopes(scopes.Select(s => s.Value()));
        }

        /// <summary>
        /// Sets the scopes requested for the OAuth token.
        /// </summary>
        /// <param name="scopes">The requested scopes.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public AuthorizationUrlBuilder AddScopes(IEnumerable<string> scopes)
        {
            if (_scopes == null)
            {
                _scopes = new HashSet<string>();
            }

            _scopes = _scopes.Concat(scopes).ToHashSet();
            return this;
        }

        /// <summary>
        /// Sets the redirect URI to return to after authentication.
        /// </summary>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public AuthorizationUrlBuilder WithRedirectUri(string redirectUri)
        {
            _redirectUri = redirectUri;
            return this;
        }

        /// <summary>
        /// Sets the state parameter for the authorization request.
        /// </summary>
        /// <param name="state">The state value.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public AuthorizationUrlBuilder WithState(string state)
        {
            _state = state;
            return this;
        }

        /// <summary>
        /// Sets the code challenge for PKCE, and also sets the code challenge method to "S256".
        /// </summary>
        /// <param name="challenge">The code challenge.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public AuthorizationUrlBuilder WithCodeChallenge(string challenge)
        {
            _codeChallenge = challenge;
            return this;
        }

        /// <summary>
        /// Builds the authorization URL.
        /// </summary>
        /// <returns>A fully formed Docusign authorization URL.</returns>
        /// <exception cref="InvalidOperationException">Thrown when required parameters are missing.</exception>
        public string Build()
        {
            var uriBuilder = new UriBuilder($"{_oauthBasePath}/oauth/auth");
            var query = HttpUtility.ParseQueryString(string.Empty);

            SetRequiredQueryParam(query, "response_type", _responseType);
            SetRequiredQueryParam(query, "client_id", _clientId);
            SetRequiredQueryParam(query, "redirect_uri", _redirectUri);

            if (_scopes == null)
            {
                AuthScope[] allScopes = Enum.GetValues<AuthScope>();
                query["scope"] = string.Join(" ", allScopes.Select(s => s.Value()));
            }
            else
            {
                query["scope"] = string.Join(" ", _scopes);
            }

            if (!string.IsNullOrWhiteSpace(_codeChallenge))
            {
                query["code_challenge"] = _codeChallenge;
                query["code_challenge_method"] = "S256";
            }

            if (!string.IsNullOrWhiteSpace(_state))
            {
                query["state"] = _state;
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri.ToString();
        }

        private static void SetRequiredQueryParam(
            NameValueCollection nameValueCollection,
            string name,
            string? value
        )
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"{name} is required.");
            }
            nameValueCollection[name] = value;
        }
    }

    public enum AuthorizationUrlResponseType
    {
        [EnumMember(Value = "code")]
        Code,

        [EnumMember(Value = "token")]
        Token,
    }
}
