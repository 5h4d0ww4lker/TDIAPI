using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IProdactCategoriesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetProdactCategoryForViewDto>> GetAll(GetAllProdactCategoriesInput input);

        Task<GetProdactCategoryForViewDto> GetProdactCategoryForView(int id);

		Task<GetProdactCategoryForEditOutput> GetProdactCategoryForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditProdactCategoryDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetProdactCategoriesToExcel(GetAllProdactCategoriesForExcelInput input);

		
    }
}