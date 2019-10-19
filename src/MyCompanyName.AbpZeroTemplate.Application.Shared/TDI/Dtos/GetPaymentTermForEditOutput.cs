using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetPaymentTermForEditOutput
    {
		public CreateOrEditPaymentTermDto PaymentTerm { get; set; }


    }
}