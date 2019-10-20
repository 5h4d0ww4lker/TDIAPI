using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("ProductSubCategories")]
    public class ProductSubCategory : FullAuditedEntity 
    {

		[Required]
		[StringLength(ProductSubCategoryConsts.MaxPipeDiameterLength, MinimumLength = ProductSubCategoryConsts.MinPipeDiameterLength)]
		public virtual string PipeDiameter { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxWallSLength, MinimumLength = ProductSubCategoryConsts.MinWallSLength)]
		public virtual string WallS { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxSDRLength, MinimumLength = ProductSubCategoryConsts.MinSDRLength)]
		public virtual string SDR { get; set; }
		
		[StringLength(ProductSubCategoryConsts.MaxPNLength, MinimumLength = ProductSubCategoryConsts.MinPNLength)]
		public virtual string PN { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxWPMLength, MinimumLength = ProductSubCategoryConsts.MinWPMLength)]
		public virtual string WPM { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxOutputKGHLength, MinimumLength = ProductSubCategoryConsts.MinOutputKGHLength)]
		public virtual string OutputKGH { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxHauiOffSpeedLength, MinimumLength = ProductSubCategoryConsts.MinHauiOffSpeedLength)]
		public virtual string HauiOffSpeed { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxOutputTALength, MinimumLength = ProductSubCategoryConsts.MinOutputTALength)]
		public virtual string OutputTA { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxProductionTimeKMALength, MinimumLength = ProductSubCategoryConsts.MinProductionTimeKMALength)]
		public virtual string ProductionTimeKMA { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxProductionPipeLengthKMALength, MinimumLength = ProductSubCategoryConsts.MinProductionPipeLengthKMALength)]
		public virtual string ProductionPipeLengthKMA { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxPipeLengthMLength, MinimumLength = ProductSubCategoryConsts.MinPipeLengthMLength)]
		public virtual string PipeLengthM { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxExtruderLength, MinimumLength = ProductSubCategoryConsts.MinExtruderLength)]
		public virtual string Extruder { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxPipeHeadLength, MinimumLength = ProductSubCategoryConsts.MinPipeHeadLength)]
		public virtual string PipeHead { get; set; }
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxUnitPriceLength, MinimumLength = ProductSubCategoryConsts.MinUnitPriceLength)]
		public virtual string UnitPrice { get; set; }
		

		public virtual int ProductCategoryId { get; set; }
		
        [ForeignKey("ProductCategoryId")]
		public ProductCategory ProductCategoryFk { get; set; }
		
    }
}