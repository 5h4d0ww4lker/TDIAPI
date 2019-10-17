using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IContactPersonsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetContactPersonForViewDto>> GetAll(GetAllContactPersonsInput input);

        Task<GetContactPersonForViewDto> GetContactPersonForView(int id);

		Task<GetContactPersonForEditOutput> GetContactPersonForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditContactPersonDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetContactPersonsToExcel(GetAllContactPersonsForExcelInput input);

		
    }
}