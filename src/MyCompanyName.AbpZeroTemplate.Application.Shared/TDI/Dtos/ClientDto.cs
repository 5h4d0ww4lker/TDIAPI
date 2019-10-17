
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class ClientDto : EntityDto
    {
		public string ClientName { get; set; }

		public string ClientTin { get; set; }

		public string ClientAddress { get; set; }

		public string ClientPhone { get; set; }


		 public int? ContactPersonId { get; set; }

		 
    }
}