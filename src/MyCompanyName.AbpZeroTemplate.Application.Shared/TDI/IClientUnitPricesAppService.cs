using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IClientUnitPricesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetClientUnitPriceForViewDto>> GetAll(GetAllClientUnitPricesInput input);

        Task<GetClientUnitPriceForViewDto> GetClientUnitPriceForView(int id);

		Task<GetClientUnitPriceForEditOutput> GetClientUnitPriceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditClientUnitPriceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetClientUnitPricesToExcel(GetAllClientUnitPricesForExcelInput input);

		
		Task<PagedResultDto<ClientUnitPriceClientLookupTableDto>> GetAllClientForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<ClientUnitPriceProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input);
		
    }
}