using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllQuotationItemsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string QuantityFilter { get; set; }

		public string TotalAmountInETBFilter { get; set; }

		public string DescriptionFilter { get; set; }


		 public string QuotationQuotationNumberFilter { get; set; }

		 		 public string ProductCategoryMaterialFilter { get; set; }

		 		 public string ProductSubCategoryPipeDiameterFilter { get; set; }

		 		 public string UnitPricePriceFilter { get; set; }

		 		 public string ClientUnitPriceDescriptionFilter { get; set; }

		 		 public string QuotationUnitPriceDescriptionFilter { get; set; }

		 
    }
}