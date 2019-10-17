using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IProductCategoriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetProductCategoryForViewDto>> GetAll(GetAllProductCategoriesInput input);

        Task<GetProductCategoryForViewDto> GetProductCategoryForView(int id);

		Task<GetProductCategoryForEditOutput> GetProductCategoryForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditProductCategoryDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetProductCategoriesToExcel(GetAllProductCategoriesForExcelInput input);

		
    }
}