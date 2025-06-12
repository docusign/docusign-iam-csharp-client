// This file is manually maintained and should not be overwritten by code generation.
// Contains custom authorization scopes for Docusign authentication.

using System;
using Newtonsoft.Json;

namespace Docusign.IAM.SDK.Models.Components
{
    public enum AuthScope
    {
        /// <summary>
        /// Allows read access to the unified repository.
        /// </summary>
        [JsonProperty("adm_store_unified_repo_read")]
        AdmStoreUnifiedRepoRead,

        /// <summary>
        /// Allows write access to the unified repository.
        /// </summary>
        [JsonProperty("adm_store_unified_repo_write")]
        AdmStoreUnifiedRepoWrite,

        /// <summary>
        /// Allows management of advanced workflow operations.
        /// </summary>
        [JsonProperty("aow_manage")]
        AowManage,

        /// <summary>
        /// Required for applications that impersonate users to perform API
        /// calls. Used in JWT Grant authentication.
        /// </summary>
        [JsonProperty("impersonation")]
        Impersonation,

        /// <summary>
        /// Used to the read model schema.
        /// </summary>
        [JsonProperty("models_read")]
        ModelsRead,

        /// <summary>
        /// Required to call most eSignature REST API endpoints
        /// </summary>
        [JsonProperty("signature")]
        Signature,
    }

    public static class AuthScopeExtension
    {
        public static string Value(this AuthScope value)
        {
            return (
                    (JsonPropertyAttribute)
                        value
                            .GetType()
                            .GetMember(value.ToString())[0]
                            .GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]
                ).PropertyName ?? value.ToString();
        }

        public static AuthScope ToEnum(this string value)
        {
            foreach (var field in typeof(AuthScope).GetFields())
            {
                var attributes = field.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
                if (attributes.Length == 0)
                {
                    continue;
                }

                var attribute = attributes[0] as JsonPropertyAttribute;
                if (attribute != null && attribute.PropertyName == value)
                {
                    var enumVal = field.GetValue(null);

                    if (enumVal is AuthScope)
                    {
                        return (AuthScope)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum AuthScope");
        }
    }
}
