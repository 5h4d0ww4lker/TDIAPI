
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class CreateOrEditPaymentTermDto : EntityDto<int?>
    {

		[Required]
		[StringLength(PaymentTermConsts.MaxDescriptionLength, MinimumLength = PaymentTermConsts.MinDescriptionLength)]
		public string Description { get; set; }
		
		

    }
}