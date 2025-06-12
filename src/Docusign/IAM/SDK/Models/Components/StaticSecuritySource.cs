// This file is manually maintained and should not be overwritten by code generation.
// Contains static security source implementation for authentication.

using System;

namespace Docusign.IAM.SDK.Models.Components
{
    public class StaticSecuritySource : ISecuritySource
    {
        private readonly string _accessToken;

        public StaticSecuritySource(string? accessToken)
        {
            _accessToken = accessToken ?? string.Empty;
        }

        public Security GetSecurity()
        {
            return new Security { AccessToken = _accessToken };
        }
    }
}

