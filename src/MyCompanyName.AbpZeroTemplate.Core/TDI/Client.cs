using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("Clients")]
    [Audited]
    public class Client : FullAuditedEntity 
    {

		[Required]
		public virtual string ClientName { get; set; }
		
		public virtual string ClientTin { get; set; }
		
		public virtual string ClientAddress { get; set; }
		
		public virtual string ClientPhone { get; set; }
		

		public virtual int? ContactPersonId { get; set; }
		
        [ForeignKey("ContactPersonId")]
		public ContactPerson ContactPersonFk { get; set; }
		
    }
}