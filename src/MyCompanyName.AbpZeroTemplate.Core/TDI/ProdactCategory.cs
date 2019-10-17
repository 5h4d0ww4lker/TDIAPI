using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("ProdactCategories")]
    public class ProdactCategory : FullAuditedEntity 
    {

		[Required]
		[StringLength(ProdactCategoryConsts.MaxMaterialLength, MinimumLength = ProdactCategoryConsts.MinMaterialLength)]
		public virtual string Material { get; set; }
		
		[Required]
		[StringLength(ProdactCategoryConsts.MaxExtruderLength, MinimumLength = ProdactCategoryConsts.MinExtruderLength)]
		public virtual string Extruder { get; set; }
		
		[Required]
		[StringLength(ProdactCategoryConsts.MaxPipeheadLength, MinimumLength = ProdactCategoryConsts.MinPipeheadLength)]
		public virtual string Pipehead { get; set; }
		

    }
}