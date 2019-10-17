
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditQuotationUnitPriceDto : EntityDto<int?>
    {

		[Required]
		[StringLength(QuotationUnitPriceConsts.MaxDescriptionLength, MinimumLength = QuotationUnitPriceConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		
		[Required]
		[StringLength(QuotationUnitPriceConsts.MaxUnitLength, MinimumLength = QuotationUnitPriceConsts.MinUnitLength)]
		public string Unit { get; set; }
		
		
		[Required]
		[StringLength(QuotationUnitPriceConsts.MaxPriceLength, MinimumLength = QuotationUnitPriceConsts.MinPriceLength)]
		public string Price { get; set; }
		
		
		 public int? QuotationId { get; set; }
		 
		 
    }
}