
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditOrderedProductDto : EntityDto<int?>
    {

		[Required]
		[StringLength(OrderedProductConsts.MaxQuantityLength, MinimumLength = OrderedProductConsts.MinQuantityLength)]
		public string Quantity { get; set; }
		
		
		[Required]
		[StringLength(OrderedProductConsts.MaxTotalAmountInETBLength, MinimumLength = OrderedProductConsts.MinTotalAmountInETBLength)]
		public string TotalAmountInETB { get; set; }
		
		
		 public int QuotationItemId { get; set; }
		 
		 
    }
}