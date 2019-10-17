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
	[AbpAuthorize(AppPermissions.Pages_UnitPrices)]
    public class UnitPricesAppService : AbpZeroTemplateAppServiceBase, IUnitPricesAppService
    {
		 private readonly IRepository<UnitPrice> _unitPriceRepository;
		 private readonly IUnitPricesExcelExporter _unitPricesExcelExporter;
		 private readonly IRepository<Product,int> _lookup_productRepository;
		 

		  public UnitPricesAppService(IRepository<UnitPrice> unitPriceRepository, IUnitPricesExcelExporter unitPricesExcelExporter , IRepository<Product, int> lookup_productRepository) 
		  {
			_unitPriceRepository = unitPriceRepository;
			_unitPricesExcelExporter = unitPricesExcelExporter;
			_lookup_productRepository = lookup_productRepository;
		
		  }

		 public async Task<PagedResultDto<GetUnitPriceForViewDto>> GetAll(GetAllUnitPricesInput input)
         {
			
			var filteredUnitPrices = _unitPriceRepository.GetAll()
						.Include( e => e.ProductFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Price.Contains(input.Filter) || e.Unit.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitFilter),  e => e.Unit.ToLower() == input.UnitFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductDescriptionFilter), e => e.ProductFk != null && e.ProductFk.Description.ToLower() == input.ProductDescriptionFilter.ToLower().Trim());

			var pagedAndFilteredUnitPrices = filteredUnitPrices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var unitPrices = from o in pagedAndFilteredUnitPrices
                         join o1 in _lookup_productRepository.GetAll() on o.ProductId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetUnitPriceForViewDto() {
							UnitPrice = new UnitPriceDto
							{
                                Price = o.Price,
                                Unit = o.Unit,
                                Id = o.Id
							},
                         	ProductDescription = s1 == null ? "" : s1.Description.ToString()
						};

            var totalCount = await filteredUnitPrices.CountAsync();

            return new PagedResultDto<GetUnitPriceForViewDto>(
                totalCount,
                await unitPrices.ToListAsync()
            );
         }
		 
		 public async Task<GetUnitPriceForViewDto> GetUnitPriceForView(int id)
         {
            var unitPrice = await _unitPriceRepository.GetAsync(id);

            var output = new GetUnitPriceForViewDto { UnitPrice = ObjectMapper.Map<UnitPriceDto>(unitPrice) };

		    if (output.UnitPrice.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((int)output.UnitPrice.ProductId);
                output.ProductDescription = _lookupProduct.Description.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_UnitPrices_Edit)]
		 public async Task<GetUnitPriceForEditOutput> GetUnitPriceForEdit(EntityDto input)
         {
            var unitPrice = await _unitPriceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetUnitPriceForEditOutput {UnitPrice = ObjectMapper.Map<CreateOrEditUnitPriceDto>(unitPrice)};

		    if (output.UnitPrice.ProductId != null)
            {
                var _lookupProduct = await _lookup_productRepository.FirstOrDefaultAsync((int)output.UnitPrice.ProductId);
                output.ProductDescription = _lookupProduct.Description.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditUnitPriceDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_UnitPrices_Create)]
		 private async Task Create(CreateOrEditUnitPriceDto input)
         {
            var unitPrice = ObjectMapper.Map<UnitPrice>(input);

			

            await _unitPriceRepository.InsertAsync(unitPrice);
         }

		 [AbpAuthorize(AppPermissions.Pages_UnitPrices_Edit)]
		 private async Task Update(CreateOrEditUnitPriceDto input)
         {
            var unitPrice = await _unitPriceRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, unitPrice);
         }

		 [AbpAuthorize(AppPermissions.Pages_UnitPrices_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _unitPriceRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetUnitPricesToExcel(GetAllUnitPricesForExcelInput input)
         {
			
			var filteredUnitPrices = _unitPriceRepository.GetAll()
						.Include( e => e.ProductFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Price.Contains(input.Filter) || e.Unit.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitFilter),  e => e.Unit.ToLower() == input.UnitFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductDescriptionFilter), e => e.ProductFk != null && e.ProductFk.Description.ToLower() == input.ProductDescriptionFilter.ToLower().Trim());

			var query = (from o in filteredUnitPrices
                         join o1 in _lookup_productRepository.GetAll() on o.ProductId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetUnitPriceForViewDto() { 
							UnitPrice = new UnitPriceDto
							{
                                Price = o.Price,
                                Unit = o.Unit,
                                Id = o.Id
							},
                         	ProductDescription = s1 == null ? "" : s1.Description.ToString()
						 });


            var unitPriceListDtos = await query.ToListAsync();

            return _unitPricesExcelExporter.ExportToFile(unitPriceListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_UnitPrices)]
         public async Task<PagedResultDto<UnitPriceProductLookupTableDto>> GetAllProductForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_productRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Description.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var productList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<UnitPriceProductLookupTableDto>();
			foreach(var product in productList){
				lookupTableDtoList.Add(new UnitPriceProductLookupTableDto
				{
					Id = product.Id,
					DisplayName = product.Description?.ToString()
				});
			}

            return new PagedResultDto<UnitPriceProductLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}