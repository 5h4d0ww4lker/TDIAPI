using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllUnitPricesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PriceFilter { get; set; }

		public string UnitFilter { get; set; }


		 public string ProductDescriptionFilter { get; set; }

		 
    }
}