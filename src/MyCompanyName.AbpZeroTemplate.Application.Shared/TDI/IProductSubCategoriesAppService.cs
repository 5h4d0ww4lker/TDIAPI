using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IProductSubCategoriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetProductSubCategoryForViewDto>> GetAll(GetAllProductSubCategoriesInput input);

        Task<GetProductSubCategoryForViewDto> GetProductSubCategoryForView(int id);

		Task<GetProductSubCategoryForEditOutput> GetProductSubCategoryForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditProductSubCategoryDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetProductSubCategoriesToExcel(GetAllProductSubCategoriesForExcelInput input);

		
		Task<PagedResultDto<ProductSubCategoryProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input);
		
    }
}