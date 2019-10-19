using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IUnitPricesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetUnitPriceForViewDto>> GetAll(GetAllUnitPricesInput input);

        Task<GetUnitPriceForViewDto> GetUnitPriceForView(int id);

		Task<GetUnitPriceForEditOutput> GetUnitPriceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditUnitPriceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetUnitPricesToExcel(GetAllUnitPricesForExcelInput input);

		
		Task<PagedResultDto<UnitPriceProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input);
		
    }
}