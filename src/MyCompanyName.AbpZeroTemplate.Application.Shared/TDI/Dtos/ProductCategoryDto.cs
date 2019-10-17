
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class ProductCategoryDto : EntityDto
    {
		public string Material { get; set; }

		public string Extruder { get; set; }

		public string Pipehead { get; set; }



    }
}