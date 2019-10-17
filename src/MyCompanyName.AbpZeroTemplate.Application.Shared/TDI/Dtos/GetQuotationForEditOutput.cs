using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetQuotationForEditOutput
    {
		public CreateOrEditQuotationDto Quotation { get; set; }

		public string ClientClientName { get; set;}


    }
}