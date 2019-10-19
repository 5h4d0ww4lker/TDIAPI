

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.TDI.Exporting;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[AbpAuthorize(AppPermissions.Pages_PaymentTerms)]
    public class PaymentTermsAppService : AbpZeroTemplateAppServiceBase, IPaymentTermsAppService
    {
		 private readonly IRepository<PaymentTerm> _paymentTermRepository;
		 private readonly IPaymentTermsExcelExporter _paymentTermsExcelExporter;
		 

		  public PaymentTermsAppService(IRepository<PaymentTerm> paymentTermRepository, IPaymentTermsExcelExporter paymentTermsExcelExporter ) 
		  {
			_paymentTermRepository = paymentTermRepository;
			_paymentTermsExcelExporter = paymentTermsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPaymentTermForViewDto>> GetAll(GetAllPaymentTermsInput input)
         {
			
			var filteredPaymentTerms = _paymentTermRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPaymentTerms = filteredPaymentTerms
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var paymentTerms = from o in pagedAndFilteredPaymentTerms
                         select new GetPaymentTermForViewDto() {
							PaymentTerm = new PaymentTermDto
							{
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPaymentTerms.CountAsync();

            return new PagedResultDto<GetPaymentTermForViewDto>(
                totalCount,
                await paymentTerms.ToListAsync()
            );
         }
		 
		 public async Task<GetPaymentTermForViewDto> GetPaymentTermForView(int id)
         {
            var paymentTerm = await _paymentTermRepository.GetAsync(id);

            var output = new GetPaymentTermForViewDto { PaymentTerm = ObjectMapper.Map<PaymentTermDto>(paymentTerm) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PaymentTerms_Edit)]
		 public async Task<GetPaymentTermForEditOutput> GetPaymentTermForEdit(EntityDto input)
         {
            var paymentTerm = await _paymentTermRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPaymentTermForEditOutput {PaymentTerm = ObjectMapper.Map<CreateOrEditPaymentTermDto>(paymentTerm)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPaymentTermDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PaymentTerms_Create)]
		 private async Task Create(CreateOrEditPaymentTermDto input)
         {
            var paymentTerm = ObjectMapper.Map<PaymentTerm>(input);

			

            await _paymentTermRepository.InsertAsync(paymentTerm);
         }

		 [AbpAuthorize(AppPermissions.Pages_PaymentTerms_Edit)]
		 private async Task Update(CreateOrEditPaymentTermDto input)
         {
            var paymentTerm = await _paymentTermRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, paymentTerm);
         }

		 [AbpAuthorize(AppPermissions.Pages_PaymentTerms_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _paymentTermRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPaymentTermsToExcel(GetAllPaymentTermsForExcelInput input)
         {
			
			var filteredPaymentTerms = _paymentTermRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPaymentTerms
                         select new GetPaymentTermForViewDto() { 
							PaymentTerm = new PaymentTermDto
							{
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var paymentTermListDtos = await query.ToListAsync();

            return _paymentTermsExcelExporter.ExportToFile(paymentTermListDtos);
         }


    }
}