using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllProductsForExcelInput
    {
		public string Filter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}