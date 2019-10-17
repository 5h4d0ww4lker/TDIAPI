using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllQuotationsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string QuotationNumberFilter { get; set; }

		public string PriceValidityFilter { get; set; }

		public string TermOfPaymentFilter { get; set; }

		public string ShipmentTypesFilter { get; set; }

		public string DiscountInPercentFilter { get; set; }

		public string DiscountInAmountFilter { get; set; }


		 public string ClientClientNameFilter { get; set; }

		 
    }
}