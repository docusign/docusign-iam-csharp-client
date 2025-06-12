// This file is manually maintained and should not be overwritten by code generation.
// Contains custom User-Agent hook implementation for Docusign requests.

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Docusign.IAM.SDK.Hooks
{
    public class UserAgentHook : IBeforeRequestHook
    {
        public UserAgentHook() { }

        public Task<HttpRequestMessage> BeforeRequestAsync(
            BeforeRequestContext hookCtx,
            HttpRequestMessage request
        )
        {
            if (!request.Headers.TryGetValues("User-Agent", out var userAgentValues))
            {
                return Task.FromResult(request);
            }
            var userAgentParts = userAgentValues.ToArray();
            if (userAgentParts.Length >= 5)
            {
                var sdkVersion = userAgentParts[1];
                var genVersion = userAgentParts[2];
                var oasVersion = userAgentParts[3];
                var sdkName = userAgentParts[4];
                var (runtime, runtimeVersion) = GetRuntimeInfo();
                var userAgent =
                    $"docusign-sdk/{oasVersion}/{sdkVersion}/csharp/{runtime}_{runtimeVersion}/{genVersion}/docusign";
                request.Headers.Remove("User-Agent");
                request.Headers.TryAddWithoutValidation("User-Agent", userAgent);
            }
            return Task.FromResult(request);
        }

        private (string Runtime, string Version) GetRuntimeInfo()
        {
            var runtime = "dotnet";
            var version = Environment.Version.ToString();
            return (runtime, version);
        }
    }
}
