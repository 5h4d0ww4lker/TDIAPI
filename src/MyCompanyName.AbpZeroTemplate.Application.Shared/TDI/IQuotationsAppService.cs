using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IQuotationsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetQuotationForViewDto>> GetAll(GetAllQuotationsInput input);

        Task<GetQuotationForViewDto> GetQuotationForView(int id);

		Task<GetQuotationForEditOutput> GetQuotationForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditQuotationDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetQuotationsToExcel(GetAllQuotationsForExcelInput input);

		
		Task<PagedResultDto<QuotationClientLookupTableDto>> GetAllClientForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<QuotationProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<QuotationPaymentTermLookupTableDto>> GetAllPaymentTermForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<QuotationPriceValidityLookupTableDto>> GetAllPriceValidityForLookupTable(GetAllForLookupTableInput input);
		
    }
}