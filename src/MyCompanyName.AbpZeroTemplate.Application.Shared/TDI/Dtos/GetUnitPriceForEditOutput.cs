using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetUnitPriceForEditOutput
    {
		public CreateOrEditUnitPriceDto UnitPrice { get; set; }

		public string ProductDescription { get; set;}


    }
}