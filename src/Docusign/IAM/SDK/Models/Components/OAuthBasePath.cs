// This file is manually maintained and should not be overwritten by code generation.
// Contains OAuth base path configuration for different Docusign environments.

using System;
using Newtonsoft.Json;

namespace Docusign.IAM.SDK.Models.Components
{
    public enum OAuthBasePath
    {
        [JsonProperty("https://account-d.docusign.com")]
        Demo,

        [JsonProperty("https://account.docusign.com")]
        Production,
    }

    public static class OAuthBasePathExtension
    {
        public static string Value(this OAuthBasePath value)
        {
            return (
                    (JsonPropertyAttribute)
                        value
                            .GetType()
                            .GetMember(value.ToString())[0]
                            .GetCustomAttributes(typeof(JsonPropertyAttribute), false)[0]
                ).PropertyName ?? value.ToString();
        }

        public static OAuthBasePath ToEnum(this string value)
        {
            foreach (var field in typeof(OAuthBasePath).GetFields())
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

                    if (enumVal is OAuthBasePath)
                    {
                        return (OAuthBasePath)enumVal;
                    }
                }
            }

            throw new Exception($"Unknown value {value} for enum OAuthBasePath");
        }
    }
}
