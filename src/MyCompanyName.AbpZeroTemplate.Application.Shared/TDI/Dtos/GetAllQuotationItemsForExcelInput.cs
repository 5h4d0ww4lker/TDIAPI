using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllQuotationItemsForExcelInput
    {
		public string Filter { get; set; }

		public string QuantityFilter { get; set; }

		public string TotalAmountInETBFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public string CustomUnitPriceFilter { get; set; }


		 public string QuotationQuotationNumberFilter { get; set; }

		 		 public string ProductCategoryMaterialFilter { get; set; }

		 		 public string ProductSubCategoryPipeDiameterFilter { get; set; }

		 
    }
}