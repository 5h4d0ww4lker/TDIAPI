
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditProdactCategoryDto : EntityDto<int?>
    {

		[Required]
		[StringLength(ProdactCategoryConsts.MaxMaterialLength, MinimumLength = ProdactCategoryConsts.MinMaterialLength)]
		public string Material { get; set; }
		
		
		[Required]
		[StringLength(ProdactCategoryConsts.MaxExtruderLength, MinimumLength = ProdactCategoryConsts.MinExtruderLength)]
		public string Extruder { get; set; }
		
		
		[Required]
		[StringLength(ProdactCategoryConsts.MaxPipeheadLength, MinimumLength = ProdactCategoryConsts.MinPipeheadLength)]
		public string Pipehead { get; set; }
		
		

    }
}