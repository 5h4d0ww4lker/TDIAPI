using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetClientUnitPriceForEditOutput
    {
		public CreateOrEditClientUnitPriceDto ClientUnitPrice { get; set; }

		public string ClientClientName { get; set;}

		public string ProductSubCategoryPipeDiameter { get; set;}


    }
}