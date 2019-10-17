
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditClientDto : EntityDto<int?>
    {

		[Required]
		public string ClientName { get; set; }
		
		
		public string ClientTin { get; set; }
		
		
		public string ClientAddress { get; set; }
		
		
		public string ClientPhone { get; set; }
		
		
		 public int? ContactPersonId { get; set; }
		 
		 
    }
}