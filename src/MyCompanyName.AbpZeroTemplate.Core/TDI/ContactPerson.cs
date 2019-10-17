using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("ContactPersons")]
    [Audited]
    public class ContactPerson : FullAuditedEntity 
    {

		public virtual string FullName { get; set; }
		
		public virtual string PhoneNumber { get; set; }
		

    }
}