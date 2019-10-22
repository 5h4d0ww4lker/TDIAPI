
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class QuotationDto : EntityDto
    {
		public string QuotationNumber { get; set; }

		public string ShipmentTypes { get; set; }

		public string DiscountInPercent { get; set; }

		public string DiscountInAmount { get; set; }

		public string PlaceOfDelivery { get; set; }

		public string Status { get; set; }

		public string CheckedBy { get; set; }

		public string ApprovedBy { get; set; }


		 public int? ClientId { get; set; }

		 		 public int ProductCategoryId { get; set; }

		 		 public int? PaymentTermId { get; set; }

		 		 public int? PriceValidityId { get; set; }

		 
    }
}