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
    using System.Collections.Generic;
    
    /// <summary>
    /// A list of workflows
    /// </summary>
    public class WorkflowList
    {

        [JsonProperty("workflows")]
        public List<Workflow>? Workflows { get; set; }
    }
}