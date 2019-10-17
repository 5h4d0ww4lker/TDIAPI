using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("OrderedProducts")]
    public class OrderedProduct : FullAuditedEntity 
    {

		[Required]
		[StringLength(OrderedProductConsts.MaxQuantityLength, MinimumLength = OrderedProductConsts.MinQuantityLength)]
		public virtual string Quantity { get; set; }
		
		[Required]
		[StringLength(OrderedProductConsts.MaxTotalAmountInETBLength, MinimumLength = OrderedProductConsts.MinTotalAmountInETBLength)]
		public virtual string TotalAmountInETB { get; set; }
		

		public virtual int QuotationItemId { get; set; }
		
        [ForeignKey("QuotationItemId")]
		public QuotationItem QuotationItemFk { get; set; }
		
    }
}