
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditUnitPriceDto : EntityDto<int?>
    {

		public string Price { get; set; }
		
		
		 public int ProductCategoryId { get; set; }
		 
		 
    }
}