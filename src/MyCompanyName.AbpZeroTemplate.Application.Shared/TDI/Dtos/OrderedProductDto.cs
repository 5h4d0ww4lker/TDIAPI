
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class OrderedProductDto : EntityDto
    {
		public string Quantity { get; set; }

		public string TotalAmountInETB { get; set; }


		 public int QuotationItemId { get; set; }

		 
    }
}