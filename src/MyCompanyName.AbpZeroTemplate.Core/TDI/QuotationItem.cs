using MyCompanyName.AbpZeroTemplate.TDI;
using MyCompanyName.AbpZeroTemplate.TDI;
using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("QuotationItems")]
    public class QuotationItem : FullAuditedEntity 
    {

		[Required]
		[StringLength(QuotationItemConsts.MaxQuantityLength, MinimumLength = QuotationItemConsts.MinQuantityLength)]
		public virtual string Quantity { get; set; }
		
		[Required]
		[StringLength(QuotationItemConsts.MaxTotalAmountInETBLength, MinimumLength = QuotationItemConsts.MinTotalAmountInETBLength)]
		public virtual string TotalAmountInETB { get; set; }
		
		[Required]
		[StringLength(QuotationItemConsts.MaxDescriptionLength, MinimumLength = QuotationItemConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		
		[StringLength(QuotationItemConsts.MaxCustomUnitPriceLength, MinimumLength = QuotationItemConsts.MinCustomUnitPriceLength)]
		public virtual string CustomUnitPrice { get; set; }
		

		public virtual int? QuotationId { get; set; }
		
        [ForeignKey("QuotationId")]
		public Quotation QuotationFk { get; set; }
		
		public virtual int? ProductCategoryId { get; set; }
		
        [ForeignKey("ProductCategoryId")]
		public ProductCategory ProductCategoryFk { get; set; }
		
		public virtual int? ProductSubCategoryId { get; set; }
		
        [ForeignKey("ProductSubCategoryId")]
		public ProductSubCategory ProductSubCategoryFk { get; set; }
		
    }
}