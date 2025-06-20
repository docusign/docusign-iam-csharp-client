//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by Speakeasy (https://speakeasy.com). DO NOT EDIT.
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace Docusign.IAM.SDK.Models.Requests
{
    using Docusign.IAM.SDK.Utils;
    
    public class GetWorkflowInstanceRequest
    {

        /// <summary>
        /// The unique identifier of the account.
        /// </summary>
        [SpeakeasyMetadata("pathParam:style=simple,explode=false,name=accountId")]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// The unique identifier of the workflow.
        /// </summary>
        [SpeakeasyMetadata("pathParam:style=simple,explode=false,name=workflowId")]
        public string WorkflowId { get; set; } = default!;

        /// <summary>
        /// Unique identifier for the workflow instance
        /// </summary>
        [SpeakeasyMetadata("pathParam:style=simple,explode=false,name=instanceId")]
        public string InstanceId { get; set; } = default!;
    }
}