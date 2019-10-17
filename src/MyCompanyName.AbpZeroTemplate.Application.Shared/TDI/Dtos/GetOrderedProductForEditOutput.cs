using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetOrderedProductForEditOutput
    {
		public CreateOrEditOrderedProductDto OrderedProduct { get; set; }

		public string QuotationItemDescription { get; set;}


    }
}