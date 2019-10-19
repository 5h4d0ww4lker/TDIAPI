using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
    public interface IPaymentTermsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPaymentTermForViewDto>> GetAll(GetAllPaymentTermsInput input);

        Task<GetPaymentTermForViewDto> GetPaymentTermForView(int id);

		Task<GetPaymentTermForEditOutput> GetPaymentTermForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPaymentTermDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPaymentTermsToExcel(GetAllPaymentTermsForExcelInput input);

		
    }
}