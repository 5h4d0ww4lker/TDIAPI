using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetProductSubCategoryForEditOutput
    {
		public CreateOrEditProductSubCategoryDto ProductSubCategory { get; set; }

		public string ProductCategoryMaterial { get; set;}


    }
}