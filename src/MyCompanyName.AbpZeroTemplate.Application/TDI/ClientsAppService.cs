using MyCompanyName.AbpZeroTemplate.TDI;


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
using System.Diagnostics;

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[AbpAuthorize(AppPermissions.Pages_Clients)]
    public class ClientsAppService : AbpZeroTemplateAppServiceBase, IClientsAppService
    {
		 private readonly IRepository<Client> _clientRepository;
		 private readonly IClientsExcelExporter _clientsExcelExporter;
		 private readonly IRepository<ContactPerson,int> _lookup_contactPersonRepository;
		 

		  public ClientsAppService(IRepository<Client> clientRepository, IClientsExcelExporter clientsExcelExporter , IRepository<ContactPerson, int> lookup_contactPersonRepository) 
		  {
			_clientRepository = clientRepository;
			_clientsExcelExporter = clientsExcelExporter;
			_lookup_contactPersonRepository = lookup_contactPersonRepository;
		
		  }

		 public async Task<PagedResultDto<GetClientForViewDto>> GetAll(GetAllClientsInput input)
         {
			
			var filteredClients = _clientRepository.GetAll()
						.Include( e => e.ContactPersonFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ClientName.Contains(input.Filter) || e.ClientTin.Contains(input.Filter) || e.ClientAddress.Contains(input.Filter) || e.ClientPhone.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientNameFilter),  e => e.ClientName.ToLower() == input.ClientNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientTinFilter),  e => e.ClientTin.ToLower() == input.ClientTinFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientAddressFilter),  e => e.ClientAddress.ToLower() == input.ClientAddressFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientPhoneFilter),  e => e.ClientPhone.ToLower() == input.ClientPhoneFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContactPersonFullNameFilter), e => e.ContactPersonFk != null && e.ContactPersonFk.FullName.ToLower() == input.ContactPersonFullNameFilter.ToLower().Trim());

			var pagedAndFilteredClients = filteredClients
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var clients = from o in pagedAndFilteredClients
                         join o1 in _lookup_contactPersonRepository.GetAll() on o.ContactPersonId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetClientForViewDto() {
							Client = new ClientDto
							{
                                ClientName = o.ClientName,
                                ClientTin = o.ClientTin,
                                ClientAddress = o.ClientAddress,
                                ClientPhone = o.ClientPhone,
                                Id = o.Id
							},
                         	ContactPersonFullName = s1 == null ? "" : s1.FullName.ToString()
						};

            var totalCount = await filteredClients.CountAsync();

            return new PagedResultDto<GetClientForViewDto>(
                totalCount,
                await clients.ToListAsync()
            );
         }
		 
		 public async Task<GetClientForViewDto> GetClientForView(int id)
         {
            var client = await _clientRepository.GetAsync(id);

            var output = new GetClientForViewDto { Client = ObjectMapper.Map<ClientDto>(client) };

		    if (output.Client.ContactPersonId != null)
            {
                var _lookupContactPerson = await _lookup_contactPersonRepository.FirstOrDefaultAsync((int)output.Client.ContactPersonId);
                output.ContactPersonFullName = _lookupContactPerson.FullName.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Clients_Edit)]
		 public async Task<GetClientForEditOutput> GetClientForEdit(EntityDto input)
         {
            var client = await _clientRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetClientForEditOutput {Client = ObjectMapper.Map<CreateOrEditClientDto>(client)};

		    if (output.Client.ContactPersonId != null)
            {
                var _lookupContactPerson = await _lookup_contactPersonRepository.FirstOrDefaultAsync((int)output.Client.ContactPersonId);
                output.ContactPersonFullName = _lookupContactPerson.FullName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditClientDto input)
         {
            Debug.WriteLine(input);
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Clients_Create)]
		 private async Task Create(CreateOrEditClientDto input)
         {
            var client = ObjectMapper.Map<Client>(input);

			

            await _clientRepository.InsertAsync(client);
         }

		 [AbpAuthorize(AppPermissions.Pages_Clients_Edit)]
		 private async Task Update(CreateOrEditClientDto input)
         {
            var client = await _clientRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, client);
         }

		 [AbpAuthorize(AppPermissions.Pages_Clients_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _clientRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetClientsToExcel(GetAllClientsForExcelInput input)
         {
			
			var filteredClients = _clientRepository.GetAll()
						.Include( e => e.ContactPersonFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ClientName.Contains(input.Filter) || e.ClientTin.Contains(input.Filter) || e.ClientAddress.Contains(input.Filter) || e.ClientPhone.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientNameFilter),  e => e.ClientName.ToLower() == input.ClientNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientTinFilter),  e => e.ClientTin.ToLower() == input.ClientTinFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientAddressFilter),  e => e.ClientAddress.ToLower() == input.ClientAddressFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientPhoneFilter),  e => e.ClientPhone.ToLower() == input.ClientPhoneFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContactPersonFullNameFilter), e => e.ContactPersonFk != null && e.ContactPersonFk.FullName.ToLower() == input.ContactPersonFullNameFilter.ToLower().Trim());

			var query = (from o in filteredClients
                         join o1 in _lookup_contactPersonRepository.GetAll() on o.ContactPersonId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetClientForViewDto() { 
							Client = new ClientDto
							{
                                ClientName = o.ClientName,
                                ClientTin = o.ClientTin,
                                ClientAddress = o.ClientAddress,
                                ClientPhone = o.ClientPhone,
                                Id = o.Id
							},
                         	ContactPersonFullName = s1 == null ? "" : s1.FullName.ToString()
						 });


            var clientListDtos = await query.ToListAsync();

            return _clientsExcelExporter.ExportToFile(clientListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Clients)]
         public async Task<PagedResultDto<ClientContactPersonLookupTableDto>> GetAllContactPersonForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_contactPersonRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.FullName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var contactPersonList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ClientContactPersonLookupTableDto>();
			foreach(var contactPerson in contactPersonList){
				lookupTableDtoList.Add(new ClientContactPersonLookupTableDto
				{
					Id = contactPerson.Id,
					DisplayName = contactPerson.FullName?.ToString()
				});
			}

            return new PagedResultDto<ClientContactPersonLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}