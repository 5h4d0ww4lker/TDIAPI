using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("Products")]
    [Audited]
    public class Product : FullAuditedEntity 
    {

		public virtual string Description { get; set; }
		

    }
}