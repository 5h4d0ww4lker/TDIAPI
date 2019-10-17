using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetQuotationUnitPriceForEditOutput
    {
		public CreateOrEditQuotationUnitPriceDto QuotationUnitPrice { get; set; }

		public string QuotationQuotationNumber { get; set;}


    }
}