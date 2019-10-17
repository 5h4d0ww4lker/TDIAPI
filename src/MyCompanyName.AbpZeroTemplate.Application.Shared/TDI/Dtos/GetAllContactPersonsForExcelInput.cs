using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllContactPersonsForExcelInput
    {
		public string Filter { get; set; }

		public string FullNameFilter { get; set; }

		public string PhoneNumberFilter { get; set; }



    }
}