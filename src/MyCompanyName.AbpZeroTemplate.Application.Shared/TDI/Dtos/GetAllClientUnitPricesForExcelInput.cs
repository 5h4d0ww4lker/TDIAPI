using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllClientUnitPricesForExcelInput
    {
		public string Filter { get; set; }

		public string DescriptionFilter { get; set; }

		public string UnitFilter { get; set; }

		public string PriceFilter { get; set; }


		 public string ClientClientNameFilter { get; set; }

		 		 public string ProductSubCategoryPipeDiameterFilter { get; set; }

		 
    }
}