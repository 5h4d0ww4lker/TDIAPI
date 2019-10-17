
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditQuotationDto : EntityDto<int?>
    {

		[Required]
		public string QuotationNumber { get; set; }
		
		
		[Required]
		[StringLength(QuotationConsts.MaxPriceValidityLength, MinimumLength = QuotationConsts.MinPriceValidityLength)]
		public string PriceValidity { get; set; }
		
		
		[Required]
		[StringLength(QuotationConsts.MaxTermOfPaymentLength, MinimumLength = QuotationConsts.MinTermOfPaymentLength)]
		public string TermOfPayment { get; set; }
		
		
		[Required]
		[StringLength(QuotationConsts.MaxShipmentTypesLength, MinimumLength = QuotationConsts.MinShipmentTypesLength)]
		public string ShipmentTypes { get; set; }
		
		
		public string DiscountInPercent { get; set; }
		
		
		public string DiscountInAmount { get; set; }
		
		
		 public int? ClientId { get; set; }
		 
		 
    }
}