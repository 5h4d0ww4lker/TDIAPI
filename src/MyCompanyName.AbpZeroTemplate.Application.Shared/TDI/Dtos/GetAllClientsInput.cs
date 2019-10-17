using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetAllClientsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ClientNameFilter { get; set; }

		public string ClientTinFilter { get; set; }

		public string ClientAddressFilter { get; set; }

		public string ClientPhoneFilter { get; set; }


		 public string ContactPersonFullNameFilter { get; set; }

		 
    }
}