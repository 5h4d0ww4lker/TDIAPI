using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("UnitPrices")]
    [Audited]
    public class UnitPrice : FullAuditedEntity 
    {

		public virtual string Price { get; set; }
		
		[Required]
		[StringLength(UnitPriceConsts.MaxUnitLength, MinimumLength = UnitPriceConsts.MinUnitLength)]
		public virtual string Unit { get; set; }
		

		public virtual int? ProductId { get; set; }
		
        [ForeignKey("ProductId")]
		public Product ProductFk { get; set; }
		
    }
}