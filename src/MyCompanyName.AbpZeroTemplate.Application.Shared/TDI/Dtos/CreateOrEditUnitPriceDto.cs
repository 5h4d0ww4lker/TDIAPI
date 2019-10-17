
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditUnitPriceDto : EntityDto<int?>
    {

		public string Price { get; set; }
		
		
		[Required]
		[StringLength(UnitPriceConsts.MaxUnitLength, MinimumLength = UnitPriceConsts.MinUnitLength)]
		public string Unit { get; set; }
		
		
		 public int? ProductId { get; set; }
		 
		 
    }
}