using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetContactPersonForEditOutput
    {
		public CreateOrEditContactPersonDto ContactPerson { get; set; }


    }
}