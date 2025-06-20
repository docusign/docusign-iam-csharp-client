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
    using NodaTime;
    using System;
    
    /// <summary>
    /// &quot;The conditions or rules written in a legal agreement. The set of possible provisions is determined by the agreement type. <br/>
    /// 
    /// <remarks>
    /// This set of provisions can change dynamically.&quot;<br/>
    /// 
    /// </remarks>
    /// </summary>
    public class Provisions
    {

        /// <summary>
        /// The type of assignment rights in the agreement (e.g., transferability)
        /// </summary>
        [JsonProperty("assignment_type")]
        public string? AssignmentType { get; set; }

        /// <summary>
        /// Provisions related to changes in control of the assigning party
        /// </summary>
        [JsonProperty("assignment_change_of_control")]
        public string? AssignmentChangeOfControl { get; set; }

        /// <summary>
        /// Provisions for the termination of assignment rights
        /// </summary>
        [JsonProperty("assignment_termination_rights")]
        public string? AssignmentTerminationRights { get; set; }

        /// <summary>
        /// A subset of ISO 8601 duration. Fractional or negative values are not supported.
        /// </summary>
        [JsonProperty("confidentiality_obligation_period")]
        public string? ConfidentialityObligationPeriod { get; set; } = null;

        /// <summary>
        /// The governing law clause identifies the substantive law that will govern the rights and obligations of the parties to the agreement.
        /// </summary>
        [JsonProperty("governing_law")]
        public string? GoverningLaw { get; set; } = null;

        /// <summary>
        /// A jurisdiction clause expressly sets out which courts or tribunals have the power to hear a dispute which arises under the agreement.
        /// </summary>
        [JsonProperty("jurisdiction")]
        public string? Jurisdiction { get; set; } = null;

        /// <summary>
        /// Type of non-disclosure agreement (e.g., unilateral, bilateral).
        /// </summary>
        [JsonProperty("nda_type")]
        public string? NdaType { get; set; } = null;

        /// <summary>
        /// Total annual value of the agreement.
        /// </summary>
        [JsonProperty("annual_agreement_value")]
        public double? AnnualAgreementValue { get; set; } = null;

        /// <summary>
        /// &apos;ISO 4217 codes. From https://en.wikipedia.org/wiki/ISO_4217<br/>
        /// 
        /// <remarks>
        /// https://www.currency-iso.org/en/home/tables/table-a1.html&apos;<br/>
        /// 
        /// </remarks>
        /// </summary>
        [JsonProperty("annual_agreement_value_currency_code")]
        public CurrencyCode? AnnualAgreementValueCurrencyCode { get; set; } = null;

        /// <summary>
        /// Total value of the agreement.
        /// </summary>
        [JsonProperty("total_agreement_value")]
        public double? TotalAgreementValue { get; set; } = null;

        /// <summary>
        /// &apos;ISO 4217 codes. From https://en.wikipedia.org/wiki/ISO_4217<br/>
        /// 
        /// <remarks>
        /// https://www.currency-iso.org/en/home/tables/table-a1.html&apos;<br/>
        /// 
        /// </remarks>
        /// </summary>
        [JsonProperty("total_agreement_value_currency_code")]
        public CurrencyCode? TotalAgreementValueCurrencyCode { get; set; } = null;

        /// <summary>
        /// Terms specifying the payment due date, based on a defined number of days or other conditions.
        /// </summary>
        [JsonProperty("payment_terms_due_date")]
        public PaymentTermsDueDate? PaymentTermsDueDate { get; set; } = Docusign.IAM.SDK.Models.Components.PaymentTermsDueDate.Other;

        /// <summary>
        /// Indicates if late payment fees can be charged.
        /// </summary>
        [JsonProperty("can_charge_late_payment_fees")]
        public bool? CanChargeLatePaymentFees { get; set; } = null;

        /// <summary>
        /// Percentage fee charged on late payments.
        /// </summary>
        [JsonProperty("late_payment_fee_percent")]
        public long? LatePaymentFeePercent { get; set; } = null;

        /// <summary>
        /// Maximum liability cap in the agreement
        /// </summary>
        [JsonProperty("liability_cap_fixed_amount")]
        public double? LiabilityCapFixedAmount { get; set; } = null;

        /// <summary>
        /// &apos;ISO 4217 codes. From https://en.wikipedia.org/wiki/ISO_4217<br/>
        /// 
        /// <remarks>
        /// https://www.currency-iso.org/en/home/tables/table-a1.html&apos;<br/>
        /// 
        /// </remarks>
        /// </summary>
        [JsonProperty("liability_cap_currency_code")]
        public CurrencyCode? LiabilityCapCurrencyCode { get; set; } = null;

        /// <summary>
        /// Multiplier applied to calculate the liability cap
        /// </summary>
        [JsonProperty("liability_cap_multiplier")]
        public double? LiabilityCapMultiplier { get; set; } = null;

        [JsonProperty("liability_cap_duration")]
        public string? LiabilityCapDuration { get; set; } = null;

        /// <summary>
        /// Maximum allowed percentage increase in prices, limited between 0 and 100.
        /// </summary>
        [JsonProperty("price_cap_percent_increase")]
        public float? PriceCapPercentIncrease { get; set; } = null;

        /// <summary>
        /// Specifies the type of renewal (e.g., automatic, manual).
        /// </summary>
        [JsonProperty("renewal_type")]
        public string? RenewalType { get; set; } = null;

        [JsonProperty("renewal_notice_period")]
        public string? RenewalNoticePeriod { get; set; } = null;

        /// <summary>
        /// Calculated field based on renewal notice period. (agreement expiration date - renewal notice period duration)
        /// </summary>
        [JsonProperty("renewal_notice_date")]
        public DateTime? RenewalNoticeDate { get; set; } = null;

        [JsonProperty("auto_renewal_term_length")]
        public string? AutoRenewalTermLength { get; set; } = null;

        [JsonProperty("renewal_extension_period")]
        public string? RenewalExtensionPeriod { get; set; } = null;

        [JsonProperty("renewal_process_owner")]
        public string? RenewalProcessOwner { get; set; } = null;

        /// <summary>
        /// Additional information related to the renewal process.
        /// </summary>
        [JsonProperty("renewal_additional_info")]
        public string? RenewalAdditionalInfo { get; set; } = null;

        /// <summary>
        /// The specific duration that a party has to give notice before terminating the agreement due to a significant breach or violation of terms. <br/>
        /// 
        /// <remarks>
        /// This period allows the other party to address the cause or prepare for termination.<br/>
        /// 
        /// </remarks>
        /// </summary>
        [JsonProperty("termination_period_for_cause")]
        public string? TerminationPeriodForCause { get; set; } = null;

        /// <summary>
        /// Specifies the required notice period that a party must provide before terminating the agreement for convenience, without cause, under the terms outlined in the contract.
        /// </summary>
        [JsonProperty("termination_period_for_convenience")]
        public string? TerminationPeriodForConvenience { get; set; } = null;

        /// <summary>
        /// The date when the terms of the agreement start to apply and become legally binding.
        /// </summary>
        [JsonProperty("effective_date")]
        public LocalDate? EffectiveDate { get; set; } = null;

        /// <summary>
        /// The date when the agreement ends and is no longer valid or enforceable.
        /// </summary>
        [JsonProperty("expiration_date")]
        public LocalDate? ExpirationDate { get; set; } = null;

        /// <summary>
        /// The date when the agreement is signed by all parties, making it officially binding. This is not necessarily the same as the effective date.
        /// </summary>
        [JsonProperty("execution_date")]
        public LocalDate? ExecutionDate { get; set; } = null;

        /// <summary>
        /// Overall duration of the agreement.
        /// </summary>
        [JsonProperty("term_length")]
        public string? TermLength { get; set; } = null;
    }
}