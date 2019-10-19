
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditClientUnitPriceDto : EntityDto<int?>
    {

		[Required]
		[StringLength(ClientUnitPriceConsts.MaxDescriptionLength, MinimumLength = ClientUnitPriceConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		
		[Required]
		[StringLength(ClientUnitPriceConsts.MaxPriceLength, MinimumLength = ClientUnitPriceConsts.MinPriceLength)]
		public string Price { get; set; }
		
		
		 public int ClientId { get; set; }
		 
		 		 public int ProductCategoryId { get; set; }
		 
		 
    }
}