using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("PriceValidities")]
    public class PriceValidity : FullAuditedEntity 
    {

		[Required]
		[StringLength(PriceValidityConsts.MaxDescriptionLength, MinimumLength = PriceValidityConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		

    }
}