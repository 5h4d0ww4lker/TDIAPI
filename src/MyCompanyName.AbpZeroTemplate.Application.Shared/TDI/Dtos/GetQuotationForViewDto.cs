namespace MyCompanyName.AbpZeroTemplate.TDI.Dtos
{
    public class GetQuotationForViewDto
    {
		public QuotationDto Quotation { get; set; }

		public string ClientClientName { get; set;}

		public string ProductCategoryMaterial { get; set;}

		public string PaymentTermDescription { get; set;}

		public string PriceValidityDescription { get; set;}


    }
}