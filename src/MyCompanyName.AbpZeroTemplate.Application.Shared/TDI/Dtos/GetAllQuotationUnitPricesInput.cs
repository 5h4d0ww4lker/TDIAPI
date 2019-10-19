using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllQuotationUnitPricesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string DescriptionFilter { get; set; }

		public string PriceFilter { get; set; }


		 public string QuotationQuotationNumberFilter { get; set; }

		 
    }
}