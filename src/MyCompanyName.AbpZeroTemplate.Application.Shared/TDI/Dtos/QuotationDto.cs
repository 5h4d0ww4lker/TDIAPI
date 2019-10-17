
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class QuotationDto : EntityDto
    {
		public string QuotationNumber { get; set; }

		public string PriceValidity { get; set; }

		public string TermOfPayment { get; set; }

		public string ShipmentTypes { get; set; }

		public string DiscountInPercent { get; set; }

		public string DiscountInAmount { get; set; }


		 public int? ClientId { get; set; }

		 
    }
}