using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("Quotations")]
    [Audited]
    public class Quotation : FullAuditedEntity 
    {

		[Required]
		public virtual string QuotationNumber { get; set; }
		
		[Required]
		[StringLength(QuotationConsts.MaxPriceValidityLength, MinimumLength = QuotationConsts.MinPriceValidityLength)]
		public virtual string PriceValidity { get; set; }
		
		[Required]
		[StringLength(QuotationConsts.MaxTermOfPaymentLength, MinimumLength = QuotationConsts.MinTermOfPaymentLength)]
		public virtual string TermOfPayment { get; set; }
		
		[Required]
		[StringLength(QuotationConsts.MaxShipmentTypesLength, MinimumLength = QuotationConsts.MinShipmentTypesLength)]
		public virtual string ShipmentTypes { get; set; }
		
		public virtual string DiscountInPercent { get; set; }
		
		public virtual string DiscountInAmount { get; set; }
		

		public virtual int? ClientId { get; set; }
		
        [ForeignKey("ClientId")]
		public Client ClientFk { get; set; }
		
    }
}