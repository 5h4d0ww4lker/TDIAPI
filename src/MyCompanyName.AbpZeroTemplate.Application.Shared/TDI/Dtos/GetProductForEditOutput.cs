using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetProductForEditOutput
    {
		public CreateOrEditProductDto Product { get; set; }


    }
}