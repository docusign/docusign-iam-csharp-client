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
    using Docusign.IAM.SDK.Utils;
    using Newtonsoft.Json;
    using System;
    
    public class ResourceMetadata
    {

        /// <summary>
        /// Timestamp when the agreement document was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; } = null;

        /// <summary>
        /// User ID of the person who created the agreement document.
        /// </summary>
        [JsonProperty("created_by")]
        public string? CreatedBy { get; set; } = null;

        /// <summary>
        /// Timestamp when the agreement document was last modified.
        /// </summary>
        [JsonProperty("modified_at")]
        public DateTime? ModifiedAt { get; set; } = null;

        /// <summary>
        /// User ID of the person who last modified the agreement document.
        /// </summary>
        [JsonProperty("modified_by")]
        public string? ModifiedBy { get; set; } = null;

        /// <summary>
        /// Unique identifier for the request, useful for tracking and debugging.
        /// </summary>
        [JsonProperty("request_id")]
        public string? RequestId { get; set; } = null;

        /// <summary>
        /// The timestamp indicating when the response was generated.
        /// </summary>
        [JsonProperty("response_timestamp")]
        public DateTime? ResponseTimestamp { get; set; } = null;

        /// <summary>
        /// The duration of time, in milliseconds, that the server took to process and respond <br/>
        /// 
        /// <remarks>
        /// to the request. This is measured from the time the server received the request <br/>
        /// until the time the response was sent.<br/>
        /// 
        /// </remarks>
        /// </summary>
        [JsonProperty("response_duration_ms")]
        public int? ResponseDurationMs { get; set; } = null;
    }
}