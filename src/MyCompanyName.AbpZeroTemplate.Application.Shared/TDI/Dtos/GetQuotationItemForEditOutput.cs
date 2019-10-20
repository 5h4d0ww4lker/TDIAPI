using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetQuotationItemForEditOutput
    {
		public CreateOrEditQuotationItemDto QuotationItem { get; set; }

		public string QuotationQuotationNumber { get; set;}

		public string ProductCategoryMaterial { get; set;}

		public string ProductSubCategoryPipeDiameter { get; set;}


    }
}