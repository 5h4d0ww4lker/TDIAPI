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
		 private readonly IRepository<ProductCategory,int> _lookup_productCategoryRepository;
		 

		  public UnitPricesAppService(IRepository<UnitPrice> unitPriceRepository, IUnitPricesExcelExporter unitPricesExcelExporter , IRepository<ProductCategory, int> lookup_productCategoryRepository) 
		  {
			_unitPriceRepository = unitPriceRepository;
			_unitPricesExcelExporter = unitPricesExcelExporter;
			_lookup_productCategoryRepository = lookup_productCategoryRepository;
		
		  }

		 public async Task<PagedResultDto<GetUnitPriceForViewDto>> GetAll(GetAllUnitPricesInput input)
         {
			
			var filteredUnitPrices = _unitPriceRepository.GetAll()
						.Include( e => e.ProductCategoryFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim());

			var pagedAndFilteredUnitPrices = filteredUnitPrices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var unitPrices = from o in pagedAndFilteredUnitPrices
                         join o1 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetUnitPriceForViewDto() {
							UnitPrice = new UnitPriceDto
							{
                                Price = o.Price,
                                Id = o.Id
							},
                         	ProductCategoryMaterial = s1 == null ? "" : s1.Material.ToString()
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

		    if (output.UnitPrice.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.UnitPrice.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_UnitPrices_Edit)]
		 public async Task<GetUnitPriceForEditOutput> GetUnitPriceForEdit(EntityDto input)
         {
            var unitPrice = await _unitPriceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetUnitPriceForEditOutput {UnitPrice = ObjectMapper.Map<CreateOrEditUnitPriceDto>(unitPrice)};

		    if (output.UnitPrice.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.UnitPrice.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
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
						.Include( e => e.ProductCategoryFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim());

			var query = (from o in filteredUnitPrices
                         join o1 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetUnitPriceForViewDto() { 
							UnitPrice = new UnitPriceDto
							{
                                Price = o.Price,
                                Id = o.Id
							},
                         	ProductCategoryMaterial = s1 == null ? "" : s1.Material.ToString()
						 });


            var unitPriceListDtos = await query.ToListAsync();

            return _unitPricesExcelExporter.ExportToFile(unitPriceListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_UnitPrices)]
         public async Task<PagedResultDto<UnitPriceProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_productCategoryRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Material.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var productCategoryList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<UnitPriceProductCategoryLookupTableDto>();
			foreach(var productCategory in productCategoryList){
				lookupTableDtoList.Add(new UnitPriceProductCategoryLookupTableDto
				{
					Id = productCategory.Id,
					DisplayName = productCategory.Material?.ToString()
				});
			}

            return new PagedResultDto<UnitPriceProductCategoryLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}