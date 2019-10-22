using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllQuotationsForExcelInput
    {
		public string Filter { get; set; }

		public string QuotationNumberFilter { get; set; }

		public string ShipmentTypesFilter { get; set; }

		public string DiscountInPercentFilter { get; set; }

		public string DiscountInAmountFilter { get; set; }

		public string PlaceOfDeliveryFilter { get; set; }

		public string StatusFilter { get; set; }

		public string CheckedByFilter { get; set; }

		public string ApprovedByFilter { get; set; }


		 public string ClientClientNameFilter { get; set; }

		 		 public string ProductCategoryMaterialFilter { get; set; }

		 		 public string PaymentTermDescriptionFilter { get; set; }

		 		 public string PriceValidityDescriptionFilter { get; set; }

		 
    }
}