
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditProductSubCategoryDto : EntityDto<int?>
    {

		[Required]
		[StringLength(ProductSubCategoryConsts.MaxPipeDiameterLength, MinimumLength = ProductSubCategoryConsts.MinPipeDiameterLength)]
		public string PipeDiameter { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxWallSLength, MinimumLength = ProductSubCategoryConsts.MinWallSLength)]
		public string WallS { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxSDRLength, MinimumLength = ProductSubCategoryConsts.MinSDRLength)]
		public string SDR { get; set; }
		
		
		[StringLength(ProductSubCategoryConsts.MaxPNLength, MinimumLength = ProductSubCategoryConsts.MinPNLength)]
		public string PN { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxWPMLength, MinimumLength = ProductSubCategoryConsts.MinWPMLength)]
		public string WPM { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxOutputKGHLength, MinimumLength = ProductSubCategoryConsts.MinOutputKGHLength)]
		public string OutputKGH { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxHauiOffSpeedLength, MinimumLength = ProductSubCategoryConsts.MinHauiOffSpeedLength)]
		public string HauiOffSpeed { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxOutputTALength, MinimumLength = ProductSubCategoryConsts.MinOutputTALength)]
		public string OutputTA { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxProductionTimeKMALength, MinimumLength = ProductSubCategoryConsts.MinProductionTimeKMALength)]
		public string ProductionTimeKMA { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxProductionPipeLengthKMALength, MinimumLength = ProductSubCategoryConsts.MinProductionPipeLengthKMALength)]
		public string ProductionPipeLengthKMA { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxPipeLengthMLength, MinimumLength = ProductSubCategoryConsts.MinPipeLengthMLength)]
		public string PipeLengthM { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxExtruderLength, MinimumLength = ProductSubCategoryConsts.MinExtruderLength)]
		public string Extruder { get; set; }
		
		
		[Required]
		[StringLength(ProductSubCategoryConsts.MaxPipeHeadLength, MinimumLength = ProductSubCategoryConsts.MinPipeHeadLength)]
		public string PipeHead { get; set; }
		
		
		 public int ProductCategoryId { get; set; }
		 
		 
    }
}