
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class UnitPriceDto : EntityDto
    {
		public string Price { get; set; }


		 public int ProductCategoryId { get; set; }

		 
    }
}