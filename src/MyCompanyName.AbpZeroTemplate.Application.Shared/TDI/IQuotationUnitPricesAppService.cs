using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IQuotationUnitPricesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetQuotationUnitPriceForViewDto>> GetAll(GetAllQuotationUnitPricesInput input);

        Task<GetQuotationUnitPriceForViewDto> GetQuotationUnitPriceForView(int id);

		Task<GetQuotationUnitPriceForEditOutput> GetQuotationUnitPriceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditQuotationUnitPriceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetQuotationUnitPricesToExcel(GetAllQuotationUnitPricesForExcelInput input);

		
		Task<PagedResultDto<QuotationUnitPriceQuotationLookupTableDto>> GetAllQuotationForLookupTable(GetAllForLookupTableInput input);
		
    }
}