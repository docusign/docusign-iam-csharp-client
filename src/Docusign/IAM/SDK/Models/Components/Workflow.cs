//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by Speakeasy (https://speakeasy.com). DO NOT EDIT.
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace Docusign.IAM.SDK.Models.Components
{
    using Docusign.IAM.SDK.Models.Components;
    using Docusign.IAM.SDK.Utils;
    using Newtonsoft.Json;
    
    public class Workflow
    {

        [JsonProperty("id")]
        public string? Id { get; set; } = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// A user-provided name for this workflow
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("account_id")]
        public string? AccountId { get; set; } = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// Indicates the readiness and deployment status of a workflow
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("metadata")]
        public ResourceMetadata? Metadata { get; set; }
    }
}