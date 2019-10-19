using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IPriceValiditiesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPriceValidityForViewDto>> GetAll(GetAllPriceValiditiesInput input);

        Task<GetPriceValidityForViewDto> GetPriceValidityForView(int id);

		Task<GetPriceValidityForEditOutput> GetPriceValidityForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPriceValidityDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPriceValiditiesToExcel(GetAllPriceValiditiesForExcelInput input);

		
    }
}