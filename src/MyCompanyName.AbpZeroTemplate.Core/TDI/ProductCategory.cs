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
		[StringLength(ProductCategoryConsts.MaxUOMLength, MinimumLength = ProductCategoryConsts.MinUOMLength)]
		public virtual string UOM { get; set; }
		
		[Required]
		[StringLength(ProductCategoryConsts.MaxDescriptionLength, MinimumLength = ProductCategoryConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		

    }
}