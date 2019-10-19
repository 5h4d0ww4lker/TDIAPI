using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("PaymentTerms")]
    public class PaymentTerm : FullAuditedEntity 
    {

		[Required]
		[StringLength(PaymentTermConsts.MaxDescriptionLength, MinimumLength = PaymentTermConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		

    }
}