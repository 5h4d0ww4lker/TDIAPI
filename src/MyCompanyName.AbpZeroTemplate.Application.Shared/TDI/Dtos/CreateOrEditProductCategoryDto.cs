
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
		[StringLength(ProductCategoryConsts.MaxUOMLength, MinimumLength = ProductCategoryConsts.MinUOMLength)]
		public string UOM { get; set; }
		
		
		[Required]
		[StringLength(ProductCategoryConsts.MaxDescriptionLength, MinimumLength = ProductCategoryConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		

    }
}