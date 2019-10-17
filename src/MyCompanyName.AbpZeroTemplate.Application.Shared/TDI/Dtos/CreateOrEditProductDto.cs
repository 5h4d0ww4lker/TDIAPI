
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditProductDto : EntityDto<int?>
    {

		public string Description { get; set; }
		
		

    }
}