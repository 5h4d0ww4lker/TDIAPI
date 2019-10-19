using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("QuotationUnitPrices")]
    public class QuotationUnitPrice : FullAuditedEntity 
    {

		[Required]
		[StringLength(QuotationUnitPriceConsts.MaxDescriptionLength, MinimumLength = QuotationUnitPriceConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		
		[Required]
		[StringLength(QuotationUnitPriceConsts.MaxPriceLength, MinimumLength = QuotationUnitPriceConsts.MinPriceLength)]
		public virtual string Price { get; set; }
		

		public virtual int? QuotationId { get; set; }
		
        [ForeignKey("QuotationId")]
		public Quotation QuotationFk { get; set; }
		
    }
}