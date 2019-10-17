
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class QuotationUnitPriceDto : EntityDto
    {
		public string Description { get; set; }

		public string Unit { get; set; }

		public string Price { get; set; }


		 public int? QuotationId { get; set; }

		 
    }
}