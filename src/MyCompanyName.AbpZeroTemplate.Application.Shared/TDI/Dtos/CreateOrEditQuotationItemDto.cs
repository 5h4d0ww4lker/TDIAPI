
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditQuotationItemDto : EntityDto<int?>
    {

		[Required]
		[StringLength(QuotationItemConsts.MaxQuantityLength, MinimumLength = QuotationItemConsts.MinQuantityLength)]
		public string Quantity { get; set; }
		
		
		[Required]
		[StringLength(QuotationItemConsts.MaxTotalAmountInETBLength, MinimumLength = QuotationItemConsts.MinTotalAmountInETBLength)]
		public string TotalAmountInETB { get; set; }
		
		
		[Required]
		[StringLength(QuotationItemConsts.MaxDescriptionLength, MinimumLength = QuotationItemConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		
		[StringLength(QuotationItemConsts.MaxCustomUnitPriceLength, MinimumLength = QuotationItemConsts.MinCustomUnitPriceLength)]
		public string CustomUnitPrice { get; set; }
		
		
		 public int? QuotationId { get; set; }
		 
		 		 public int? ProductCategoryId { get; set; }
		 
		 		 public int? ProductSubCategoryId { get; set; }
		 
		 
    }
}