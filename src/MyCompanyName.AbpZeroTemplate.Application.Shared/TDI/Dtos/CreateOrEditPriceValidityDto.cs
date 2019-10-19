
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditPriceValidityDto : EntityDto<int?>
    {

		[Required]
		[StringLength(PriceValidityConsts.MaxDescriptionLength, MinimumLength = PriceValidityConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		

    }
}