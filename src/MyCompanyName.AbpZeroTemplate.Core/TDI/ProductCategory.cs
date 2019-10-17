using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("ProductCategories")]
    [Audited]
    public class ProductCategory : FullAuditedEntity 
    {

		[Required]
		[StringLength(ProductCategoryConsts.MaxMaterialLength, MinimumLength = ProductCategoryConsts.MinMaterialLength)]
		public virtual string Material { get; set; }
		
		[Required]
		[StringLength(ProductCategoryConsts.MaxExtruderLength, MinimumLength = ProductCategoryConsts.MinExtruderLength)]
		public virtual string Extruder { get; set; }
		
		[Required]
		[StringLength(ProductCategoryConsts.MaxPipeheadLength, MinimumLength = ProductCategoryConsts.MinPipeheadLength)]
		public virtual string Pipehead { get; set; }
		

    }
}