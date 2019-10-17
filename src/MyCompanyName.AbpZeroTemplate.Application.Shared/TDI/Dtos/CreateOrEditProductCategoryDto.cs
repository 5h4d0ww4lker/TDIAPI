
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditProductCategoryDto : EntityDto<int?>
    {

		[Required]
		[StringLength(ProductCategoryConsts.MaxMaterialLength, MinimumLength = ProductCategoryConsts.MinMaterialLength)]
		public string Material { get; set; }
		
		
		[Required]
		[StringLength(ProductCategoryConsts.MaxExtruderLength, MinimumLength = ProductCategoryConsts.MinExtruderLength)]
		public string Extruder { get; set; }
		
		
		[Required]
		[StringLength(ProductCategoryConsts.MaxPipeheadLength, MinimumLength = ProductCategoryConsts.MinPipeheadLength)]
		public string Pipehead { get; set; }
		
		

    }
}