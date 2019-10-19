
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class QuotationItemDto : EntityDto
    {
		public string Quantity { get; set; }

		public string TotalAmountInETB { get; set; }

		public string Description { get; set; }


		 public int? QuotationId { get; set; }

		 		 public int? ProductCategoryId { get; set; }

		 		 public int? ProductSubCategoryId { get; set; }

		 		 public int? UnitPriceId { get; set; }

		 		 public int? ClientUnitPriceId { get; set; }

		 		 public int? QuotationUnitPriceId { get; set; }

		 
    }
}