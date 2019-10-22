
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
		[StringLength(QuotationConsts.MaxShipmentTypesLength, MinimumLength = QuotationConsts.MinShipmentTypesLength)]
		public string ShipmentTypes { get; set; }
		
		
		public string DiscountInPercent { get; set; }
		
		
		public string DiscountInAmount { get; set; }
		
		
		public string PlaceOfDelivery { get; set; }
		
		
		[Required]
		[StringLength(QuotationConsts.MaxStatusLength, MinimumLength = QuotationConsts.MinStatusLength)]
		public string Status { get; set; }
		
		
		public string CheckedBy { get; set; }
		
		
		public string ApprovedBy { get; set; }
		
		
		 public int? ClientId { get; set; }
		 
		 		 public int ProductCategoryId { get; set; }
		 
		 		 public int? PaymentTermId { get; set; }
		 
		 		 public int? PriceValidityId { get; set; }
		 
		 
    }
}