using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IClientsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetClientForViewDto>> GetAll(GetAllClientsInput input);

        Task<GetClientForViewDto> GetClientForView(int id);

		Task<GetClientForEditOutput> GetClientForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditClientDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetClientsToExcel(GetAllClientsForExcelInput input);

		
		Task<PagedResultDto<ClientContactPersonLookupTableDto>> GetAllContactPersonForLookupTable(GetAllForLookupTableInput input);
		
    }
}