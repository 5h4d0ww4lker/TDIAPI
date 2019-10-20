using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IQuotationItemsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetQuotationItemForViewDto>> GetAll(GetAllQuotationItemsInput input);

        Task<GetQuotationItemForViewDto> GetQuotationItemForView(int id);

		Task<GetQuotationItemForEditOutput> GetQuotationItemForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditQuotationItemDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetQuotationItemsToExcel(GetAllQuotationItemsForExcelInput input);

		
		Task<PagedResultDto<QuotationItemQuotationLookupTableDto>> GetAllQuotationForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<QuotationItemProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<QuotationItemProductSubCategoryLookupTableDto>> GetAllProductSubCategoryForLookupTable(GetAllForLookupTableInput input);
		
    }
}