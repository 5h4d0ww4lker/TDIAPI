
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class ClientUnitPriceDto : EntityDto
    {
		public string Description { get; set; }

		public string Price { get; set; }


		 public int ClientId { get; set; }

		 		 public int ProductCategoryId { get; set; }

		 
    }
}