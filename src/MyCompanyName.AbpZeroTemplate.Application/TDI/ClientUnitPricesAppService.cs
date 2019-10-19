using MyCompanyName.AbpZeroTemplate.TDI;
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

namespace MyCompanyName.AbpZeroTemplate.TDI
{
	[AbpAuthorize(AppPermissions.Pages_ClientUnitPrices)]
    public class ClientUnitPricesAppService : AbpZeroTemplateAppServiceBase, IClientUnitPricesAppService
    {
		 private readonly IRepository<ClientUnitPrice> _clientUnitPriceRepository;
		 private readonly IClientUnitPricesExcelExporter _clientUnitPricesExcelExporter;
		 private readonly IRepository<Client,int> _lookup_clientRepository;
		 private readonly IRepository<ProductCategory,int> _lookup_productCategoryRepository;
		 

		  public ClientUnitPricesAppService(IRepository<ClientUnitPrice> clientUnitPriceRepository, IClientUnitPricesExcelExporter clientUnitPricesExcelExporter , IRepository<Client, int> lookup_clientRepository, IRepository<ProductCategory, int> lookup_productCategoryRepository) 
		  {
			_clientUnitPriceRepository = clientUnitPriceRepository;
			_clientUnitPricesExcelExporter = clientUnitPricesExcelExporter;
			_lookup_clientRepository = lookup_clientRepository;
		_lookup_productCategoryRepository = lookup_productCategoryRepository;
		
		  }

		 public async Task<PagedResultDto<GetClientUnitPriceForViewDto>> GetAll(GetAllClientUnitPricesInput input)
         {
			
			var filteredClientUnitPrices = _clientUnitPriceRepository.GetAll()
						.Include( e => e.ClientFk)
						.Include( e => e.ProductCategoryFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter) || e.Price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientClientNameFilter), e => e.ClientFk != null && e.ClientFk.ClientName.ToLower() == input.ClientClientNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim());

			var pagedAndFilteredClientUnitPrices = filteredClientUnitPrices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var clientUnitPrices = from o in pagedAndFilteredClientUnitPrices
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetClientUnitPriceForViewDto() {
							ClientUnitPrice = new ClientUnitPriceDto
							{
                                Description = o.Description,
                                Price = o.Price,
                                Id = o.Id
							},
                         	ClientClientName = s1 == null ? "" : s1.ClientName.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString()
						};

            var totalCount = await filteredClientUnitPrices.CountAsync();

            return new PagedResultDto<GetClientUnitPriceForViewDto>(
                totalCount,
                await clientUnitPrices.ToListAsync()
            );
         }
		 
		 public async Task<GetClientUnitPriceForViewDto> GetClientUnitPriceForView(int id)
         {
            var clientUnitPrice = await _clientUnitPriceRepository.GetAsync(id);

            var output = new GetClientUnitPriceForViewDto { ClientUnitPrice = ObjectMapper.Map<ClientUnitPriceDto>(clientUnitPrice) };

		    if (output.ClientUnitPrice.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((int)output.ClientUnitPrice.ClientId);
                output.ClientClientName = _lookupClient.ClientName.ToString();
            }

		    if (output.ClientUnitPrice.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.ClientUnitPrice.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ClientUnitPrices_Edit)]
		 public async Task<GetClientUnitPriceForEditOutput> GetClientUnitPriceForEdit(EntityDto input)
         {
            var clientUnitPrice = await _clientUnitPriceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetClientUnitPriceForEditOutput {ClientUnitPrice = ObjectMapper.Map<CreateOrEditClientUnitPriceDto>(clientUnitPrice)};

		    if (output.ClientUnitPrice.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((int)output.ClientUnitPrice.ClientId);
                output.ClientClientName = _lookupClient.ClientName.ToString();
            }

		    if (output.ClientUnitPrice.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.ClientUnitPrice.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditClientUnitPriceDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ClientUnitPrices_Create)]
		 private async Task Create(CreateOrEditClientUnitPriceDto input)
         {
            var clientUnitPrice = ObjectMapper.Map<ClientUnitPrice>(input);

			

            await _clientUnitPriceRepository.InsertAsync(clientUnitPrice);
         }

		 [AbpAuthorize(AppPermissions.Pages_ClientUnitPrices_Edit)]
		 private async Task Update(CreateOrEditClientUnitPriceDto input)
         {
            var clientUnitPrice = await _clientUnitPriceRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, clientUnitPrice);
         }

		 [AbpAuthorize(AppPermissions.Pages_ClientUnitPrices_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _clientUnitPriceRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetClientUnitPricesToExcel(GetAllClientUnitPricesForExcelInput input)
         {
			
			var filteredClientUnitPrices = _clientUnitPriceRepository.GetAll()
						.Include( e => e.ClientFk)
						.Include( e => e.ProductCategoryFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter) || e.Price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientClientNameFilter), e => e.ClientFk != null && e.ClientFk.ClientName.ToLower() == input.ClientClientNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim());

			var query = (from o in filteredClientUnitPrices
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetClientUnitPriceForViewDto() { 
							ClientUnitPrice = new ClientUnitPriceDto
							{
                                Description = o.Description,
                                Price = o.Price,
                                Id = o.Id
							},
                         	ClientClientName = s1 == null ? "" : s1.ClientName.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString()
						 });


            var clientUnitPriceListDtos = await query.ToListAsync();

            return _clientUnitPricesExcelExporter.ExportToFile(clientUnitPriceListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_ClientUnitPrices)]
         public async Task<PagedResultDto<ClientUnitPriceClientLookupTableDto>> GetAllClientForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_clientRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ClientName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var clientList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ClientUnitPriceClientLookupTableDto>();
			foreach(var client in clientList){
				lookupTableDtoList.Add(new ClientUnitPriceClientLookupTableDto
				{
					Id = client.Id,
					DisplayName = client.ClientName?.ToString()
				});
			}

            return new PagedResultDto<ClientUnitPriceClientLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_ClientUnitPrices)]
         public async Task<PagedResultDto<ClientUnitPriceProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_productCategoryRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Material.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var productCategoryList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ClientUnitPriceProductCategoryLookupTableDto>();
			foreach(var productCategory in productCategoryList){
				lookupTableDtoList.Add(new ClientUnitPriceProductCategoryLookupTableDto
				{
					Id = productCategory.Id,
					DisplayName = productCategory.Material?.ToString()
				});
			}

            return new PagedResultDto<ClientUnitPriceProductCategoryLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}