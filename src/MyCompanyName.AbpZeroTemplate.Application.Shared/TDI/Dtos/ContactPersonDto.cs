
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class ContactPersonDto : EntityDto
    {
		public string FullName { get; set; }

		public string PhoneNumber { get; set; }



    }
}