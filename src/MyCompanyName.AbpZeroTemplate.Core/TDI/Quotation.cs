using MyCompanyName.AbpZeroTemplate.TDI;
using MyCompanyName.AbpZeroTemplate.TDI;
using MyCompanyName.AbpZeroTemplate.TDI;
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
		[StringLength(QuotationConsts.MaxShipmentTypesLength, MinimumLength = QuotationConsts.MinShipmentTypesLength)]
		public virtual string ShipmentTypes { get; set; }
		
		public virtual string DiscountInPercent { get; set; }
		
		public virtual string DiscountInAmount { get; set; }
		
		public virtual string PlaceOfDelivery { get; set; }
		
		[Required]
		[StringLength(QuotationConsts.MaxStatusLength, MinimumLength = QuotationConsts.MinStatusLength)]
		public virtual string Status { get; set; }
		
		public virtual string CheckedBy { get; set; }
		
		public virtual string ApprovedBy { get; set; }
		

		public virtual int? ClientId { get; set; }
		
        [ForeignKey("ClientId")]
		public Client ClientFk { get; set; }
		
		public virtual int ProductCategoryId { get; set; }
		
        [ForeignKey("ProductCategoryId")]
		public ProductCategory ProductCategoryFk { get; set; }
		
		public virtual int? PaymentTermId { get; set; }
		
        [ForeignKey("PaymentTermId")]
		public PaymentTerm PaymentTermFk { get; set; }
		
		public virtual int? PriceValidityId { get; set; }
		
        [ForeignKey("PriceValidityId")]
		public PriceValidity PriceValidityFk { get; set; }
		
    }
}