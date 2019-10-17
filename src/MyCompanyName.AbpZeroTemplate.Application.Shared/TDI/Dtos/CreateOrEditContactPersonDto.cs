
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditContactPersonDto : EntityDto<int?>
    {

		public string FullName { get; set; }
		
		
		public string PhoneNumber { get; set; }
		
		

    }
}