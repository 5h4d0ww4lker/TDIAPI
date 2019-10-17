using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IOrderedProductsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetOrderedProductForViewDto>> GetAll(GetAllOrderedProductsInput input);

        Task<GetOrderedProductForViewDto> GetOrderedProductForView(int id);

		Task<GetOrderedProductForEditOutput> GetOrderedProductForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditOrderedProductDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetOrderedProductsToExcel(GetAllOrderedProductsForExcelInput input);

		
		Task<PagedResultDto<OrderedProductQuotationItemLookupTableDto>> GetAllQuotationItemForLookupTable(GetAllForLookupTableInput input);
		
    }
}