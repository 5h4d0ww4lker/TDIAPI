using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllOrderedProductsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string QuantityFilter { get; set; }

		public string TotalAmountInETBFilter { get; set; }


		 public string QuotationItemDescriptionFilter { get; set; }

		 
    }
}