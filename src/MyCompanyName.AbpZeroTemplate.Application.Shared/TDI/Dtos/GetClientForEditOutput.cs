using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetClientForEditOutput
    {
		public CreateOrEditClientDto Client { get; set; }

		public string ContactPersonFullName { get; set;}


    }
}