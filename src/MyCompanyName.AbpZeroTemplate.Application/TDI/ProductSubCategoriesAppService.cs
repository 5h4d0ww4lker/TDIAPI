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
	[AbpAuthorize(AppPermissions.Pages_ProductSubCategories)]
    public class ProductSubCategoriesAppService : AbpZeroTemplateAppServiceBase, IProductSubCategoriesAppService
    {
		 private readonly IRepository<ProductSubCategory> _productSubCategoryRepository;
		 private readonly IProductSubCategoriesExcelExporter _productSubCategoriesExcelExporter;
		 private readonly IRepository<ProductCategory,int> _lookup_productCategoryRepository;
		 

		  public ProductSubCategoriesAppService(IRepository<ProductSubCategory> productSubCategoryRepository, IProductSubCategoriesExcelExporter productSubCategoriesExcelExporter , IRepository<ProductCategory, int> lookup_productCategoryRepository) 
		  {
			_productSubCategoryRepository = productSubCategoryRepository;
			_productSubCategoriesExcelExporter = productSubCategoriesExcelExporter;
			_lookup_productCategoryRepository = lookup_productCategoryRepository;
		
		  }

		 public async Task<PagedResultDto<GetProductSubCategoryForViewDto>> GetAll(GetAllProductSubCategoriesInput input)
         {
			
			var filteredProductSubCategories = _productSubCategoryRepository.GetAll()
						.Include( e => e.ProductCategoryFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PipeDiameter.Contains(input.Filter) || e.WallS.Contains(input.Filter) || e.SDR.Contains(input.Filter) || e.PN.Contains(input.Filter) || e.WPM.Contains(input.Filter) || e.OutputKGH.Contains(input.Filter) || e.HauiOffSpeed.Contains(input.Filter) || e.OutputTA.Contains(input.Filter) || e.ProductionTimeKMA.Contains(input.Filter) || e.ProductionPipeLengthKMA.Contains(input.Filter) || e.PipeLengthM.Contains(input.Filter) || e.Extruder.Contains(input.Filter) || e.PipeHead.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeDiameterFilter),  e => e.PipeDiameter.ToLower() == input.PipeDiameterFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WallSFilter),  e => e.WallS.ToLower() == input.WallSFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SDRFilter),  e => e.SDR.ToLower() == input.SDRFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PNFilter),  e => e.PN.ToLower() == input.PNFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WPMFilter),  e => e.WPM.ToLower() == input.WPMFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OutputKGHFilter),  e => e.OutputKGH.ToLower() == input.OutputKGHFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.HauiOffSpeedFilter),  e => e.HauiOffSpeed.ToLower() == input.HauiOffSpeedFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OutputTAFilter),  e => e.OutputTA.ToLower() == input.OutputTAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductionTimeKMAFilter),  e => e.ProductionTimeKMA.ToLower() == input.ProductionTimeKMAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductionPipeLengthKMAFilter),  e => e.ProductionPipeLengthKMA.ToLower() == input.ProductionPipeLengthKMAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeLengthMFilter),  e => e.PipeLengthM.ToLower() == input.PipeLengthMFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ExtruderFilter),  e => e.Extruder.ToLower() == input.ExtruderFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeHeadFilter),  e => e.PipeHead.ToLower() == input.PipeHeadFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim());

			var pagedAndFilteredProductSubCategories = filteredProductSubCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var productSubCategories = from o in pagedAndFilteredProductSubCategories
                         join o1 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetProductSubCategoryForViewDto() {
							ProductSubCategory = new ProductSubCategoryDto
							{
                                PipeDiameter = o.PipeDiameter,
                                WallS = o.WallS,
                                SDR = o.SDR,
                                PN = o.PN,
                                WPM = o.WPM,
                                OutputKGH = o.OutputKGH,
                                HauiOffSpeed = o.HauiOffSpeed,
                                OutputTA = o.OutputTA,
                                ProductionTimeKMA = o.ProductionTimeKMA,
                                ProductionPipeLengthKMA = o.ProductionPipeLengthKMA,
                                PipeLengthM = o.PipeLengthM,
                                Extruder = o.Extruder,
                                PipeHead = o.PipeHead,
                                Id = o.Id
							},
                         	ProductCategoryMaterial = s1 == null ? "" : s1.Material.ToString()
						};

            var totalCount = await filteredProductSubCategories.CountAsync();

            return new PagedResultDto<GetProductSubCategoryForViewDto>(
                totalCount,
                await productSubCategories.ToListAsync()
            );
         }
		 
		 public async Task<GetProductSubCategoryForViewDto> GetProductSubCategoryForView(int id)
         {
            var productSubCategory = await _productSubCategoryRepository.GetAsync(id);

            var output = new GetProductSubCategoryForViewDto { ProductSubCategory = ObjectMapper.Map<ProductSubCategoryDto>(productSubCategory) };

		    if (output.ProductSubCategory.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.ProductSubCategory.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ProductSubCategories_Edit)]
		 public async Task<GetProductSubCategoryForEditOutput> GetProductSubCategoryForEdit(EntityDto input)
         {
            var productSubCategory = await _productSubCategoryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetProductSubCategoryForEditOutput {ProductSubCategory = ObjectMapper.Map<CreateOrEditProductSubCategoryDto>(productSubCategory)};

		    if (output.ProductSubCategory.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.ProductSubCategory.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditProductSubCategoryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ProductSubCategories_Create)]
		 private async Task Create(CreateOrEditProductSubCategoryDto input)
         {
            var productSubCategory = ObjectMapper.Map<ProductSubCategory>(input);

			

            await _productSubCategoryRepository.InsertAsync(productSubCategory);
         }

		 [AbpAuthorize(AppPermissions.Pages_ProductSubCategories_Edit)]
		 private async Task Update(CreateOrEditProductSubCategoryDto input)
         {
            var productSubCategory = await _productSubCategoryRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, productSubCategory);
         }

		 [AbpAuthorize(AppPermissions.Pages_ProductSubCategories_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _productSubCategoryRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetProductSubCategoriesToExcel(GetAllProductSubCategoriesForExcelInput input)
         {
			
			var filteredProductSubCategories = _productSubCategoryRepository.GetAll()
						.Include( e => e.ProductCategoryFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PipeDiameter.Contains(input.Filter) || e.WallS.Contains(input.Filter) || e.SDR.Contains(input.Filter) || e.PN.Contains(input.Filter) || e.WPM.Contains(input.Filter) || e.OutputKGH.Contains(input.Filter) || e.HauiOffSpeed.Contains(input.Filter) || e.OutputTA.Contains(input.Filter) || e.ProductionTimeKMA.Contains(input.Filter) || e.ProductionPipeLengthKMA.Contains(input.Filter) || e.PipeLengthM.Contains(input.Filter) || e.Extruder.Contains(input.Filter) || e.PipeHead.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeDiameterFilter),  e => e.PipeDiameter.ToLower() == input.PipeDiameterFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WallSFilter),  e => e.WallS.ToLower() == input.WallSFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SDRFilter),  e => e.SDR.ToLower() == input.SDRFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PNFilter),  e => e.PN.ToLower() == input.PNFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WPMFilter),  e => e.WPM.ToLower() == input.WPMFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OutputKGHFilter),  e => e.OutputKGH.ToLower() == input.OutputKGHFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.HauiOffSpeedFilter),  e => e.HauiOffSpeed.ToLower() == input.HauiOffSpeedFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.OutputTAFilter),  e => e.OutputTA.ToLower() == input.OutputTAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductionTimeKMAFilter),  e => e.ProductionTimeKMA.ToLower() == input.ProductionTimeKMAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductionPipeLengthKMAFilter),  e => e.ProductionPipeLengthKMA.ToLower() == input.ProductionPipeLengthKMAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeLengthMFilter),  e => e.PipeLengthM.ToLower() == input.PipeLengthMFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ExtruderFilter),  e => e.Extruder.ToLower() == input.ExtruderFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeHeadFilter),  e => e.PipeHead.ToLower() == input.PipeHeadFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim());

			var query = (from o in filteredProductSubCategories
                         join o1 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetProductSubCategoryForViewDto() { 
							ProductSubCategory = new ProductSubCategoryDto
							{
                                PipeDiameter = o.PipeDiameter,
                                WallS = o.WallS,
                                SDR = o.SDR,
                                PN = o.PN,
                                WPM = o.WPM,
                                OutputKGH = o.OutputKGH,
                                HauiOffSpeed = o.HauiOffSpeed,
                                OutputTA = o.OutputTA,
                                ProductionTimeKMA = o.ProductionTimeKMA,
                                ProductionPipeLengthKMA = o.ProductionPipeLengthKMA,
                                PipeLengthM = o.PipeLengthM,
                                Extruder = o.Extruder,
                                PipeHead = o.PipeHead,
                                Id = o.Id
							},
                         	ProductCategoryMaterial = s1 == null ? "" : s1.Material.ToString()
						 });


            var productSubCategoryListDtos = await query.ToListAsync();

            return _productSubCategoriesExcelExporter.ExportToFile(productSubCategoryListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_ProductSubCategories)]
         public async Task<PagedResultDto<ProductSubCategoryProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_productCategoryRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Material.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var productCategoryList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ProductSubCategoryProductCategoryLookupTableDto>();
			foreach(var productCategory in productCategoryList){
				lookupTableDtoList.Add(new ProductSubCategoryProductCategoryLookupTableDto
				{
					Id = productCategory.Id,
					DisplayName = productCategory.Material?.ToString()
				});
			}

            return new PagedResultDto<ProductSubCategoryProductCategoryLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}