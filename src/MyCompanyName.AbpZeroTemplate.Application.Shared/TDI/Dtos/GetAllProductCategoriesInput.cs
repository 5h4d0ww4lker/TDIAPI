using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllProductCategoriesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string MaterialFilter { get; set; }

		public string UOMFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}