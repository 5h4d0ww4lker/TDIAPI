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
		

		public virtual int ProductCategoryId { get; set; }
		
        [ForeignKey("ProductCategoryId")]
		public ProductCategory ProductCategoryFk { get; set; }
		
    }
}