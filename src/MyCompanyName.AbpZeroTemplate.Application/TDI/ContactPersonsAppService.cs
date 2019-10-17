

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
	[AbpAuthorize(AppPermissions.Pages_ContactPersons)]
    public class ContactPersonsAppService : AbpZeroTemplateAppServiceBase, IContactPersonsAppService
    {
		 private readonly IRepository<ContactPerson> _contactPersonRepository;
		 private readonly IContactPersonsExcelExporter _contactPersonsExcelExporter;
		 

		  public ContactPersonsAppService(IRepository<ContactPerson> contactPersonRepository, IContactPersonsExcelExporter contactPersonsExcelExporter ) 
		  {
			_contactPersonRepository = contactPersonRepository;
			_contactPersonsExcelExporter = contactPersonsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetContactPersonForViewDto>> GetAll(GetAllContactPersonsInput input)
         {
			
			var filteredContactPersons = _contactPersonRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.FullName.Contains(input.Filter) || e.PhoneNumber.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter),  e => e.FullName.ToLower() == input.FullNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNumberFilter),  e => e.PhoneNumber.ToLower() == input.PhoneNumberFilter.ToLower().Trim());

			var pagedAndFilteredContactPersons = filteredContactPersons
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var contactPersons = from o in pagedAndFilteredContactPersons
                         select new GetContactPersonForViewDto() {
							ContactPerson = new ContactPersonDto
							{
                                FullName = o.FullName,
                                PhoneNumber = o.PhoneNumber,
                                Id = o.Id
							}
						};

            var totalCount = await filteredContactPersons.CountAsync();

            return new PagedResultDto<GetContactPersonForViewDto>(
                totalCount,
                await contactPersons.ToListAsync()
            );
         }
		 
		 public async Task<GetContactPersonForViewDto> GetContactPersonForView(int id)
         {
            var contactPerson = await _contactPersonRepository.GetAsync(id);

            var output = new GetContactPersonForViewDto { ContactPerson = ObjectMapper.Map<ContactPersonDto>(contactPerson) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ContactPersons_Edit)]
		 public async Task<GetContactPersonForEditOutput> GetContactPersonForEdit(EntityDto input)
         {
            var contactPerson = await _contactPersonRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetContactPersonForEditOutput {ContactPerson = ObjectMapper.Map<CreateOrEditContactPersonDto>(contactPerson)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditContactPersonDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ContactPersons_Create)]
		 private async Task Create(CreateOrEditContactPersonDto input)
         {
            var contactPerson = ObjectMapper.Map<ContactPerson>(input);

			

            await _contactPersonRepository.InsertAsync(contactPerson);
         }

		 [AbpAuthorize(AppPermissions.Pages_ContactPersons_Edit)]
		 private async Task Update(CreateOrEditContactPersonDto input)
         {
            var contactPerson = await _contactPersonRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, contactPerson);
         }

		 [AbpAuthorize(AppPermissions.Pages_ContactPersons_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _contactPersonRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetContactPersonsToExcel(GetAllContactPersonsForExcelInput input)
         {
			
			var filteredContactPersons = _contactPersonRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.FullName.Contains(input.Filter) || e.PhoneNumber.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter),  e => e.FullName.ToLower() == input.FullNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNumberFilter),  e => e.PhoneNumber.ToLower() == input.PhoneNumberFilter.ToLower().Trim());

			var query = (from o in filteredContactPersons
                         select new GetContactPersonForViewDto() { 
							ContactPerson = new ContactPersonDto
							{
                                FullName = o.FullName,
                                PhoneNumber = o.PhoneNumber,
                                Id = o.Id
							}
						 });


            var contactPersonListDtos = await query.ToListAsync();

            return _contactPersonsExcelExporter.ExportToFile(contactPersonListDtos);
         }


    }
}