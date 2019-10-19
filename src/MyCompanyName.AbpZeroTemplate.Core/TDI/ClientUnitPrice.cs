using MyCompanyName.AbpZeroTemplate.TDI;
using MyCompanyName.AbpZeroTemplate.TDI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[Table("ClientUnitPrices")]
    public class ClientUnitPrice : FullAuditedEntity 
    {

		[Required]
		[StringLength(ClientUnitPriceConsts.MaxDescriptionLength, MinimumLength = ClientUnitPriceConsts.MinDescriptionLength)]
		public virtual string Description { get; set; }
		
		[Required]
		[StringLength(ClientUnitPriceConsts.MaxPriceLength, MinimumLength = ClientUnitPriceConsts.MinPriceLength)]
		public virtual string Price { get; set; }
		

		public virtual int ClientId { get; set; }
		
        [ForeignKey("ClientId")]
		public Client ClientFk { get; set; }
		
		public virtual int ProductCategoryId { get; set; }
		
        [ForeignKey("ProductCategoryId")]
		public ProductCategory ProductCategoryFk { get; set; }
		
    }
}