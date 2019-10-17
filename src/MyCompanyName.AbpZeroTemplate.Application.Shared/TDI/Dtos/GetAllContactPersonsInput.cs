using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllContactPersonsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string FullNameFilter { get; set; }

		public string PhoneNumberFilter { get; set; }



    }
}