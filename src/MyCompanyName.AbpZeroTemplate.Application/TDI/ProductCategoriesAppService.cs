

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
	[AbpAuthorize(AppPermissions.Pages_ProductCategories)]
    public class ProductCategoriesAppService : AbpZeroTemplateAppServiceBase, IProductCategoriesAppService
    {
		 private readonly IRepository<ProductCategory> _productCategoryRepository;
		 private readonly IProductCategoriesExcelExporter _productCategoriesExcelExporter;
		 

		  public ProductCategoriesAppService(IRepository<ProductCategory> productCategoryRepository, IProductCategoriesExcelExporter productCategoriesExcelExporter ) 
		  {
			_productCategoryRepository = productCategoryRepository;
			_productCategoriesExcelExporter = productCategoriesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetProductCategoryForViewDto>> GetAll(GetAllProductCategoriesInput input)
         {
			
			var filteredProductCategories = _productCategoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Material.Contains(input.Filter) || e.Extruder.Contains(input.Filter) || e.Pipehead.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter),  e => e.Material.ToLower() == input.MaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ExtruderFilter),  e => e.Extruder.ToLower() == input.ExtruderFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeheadFilter),  e => e.Pipehead.ToLower() == input.PipeheadFilter.ToLower().Trim());

			var pagedAndFilteredProductCategories = filteredProductCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var productCategories = from o in pagedAndFilteredProductCategories
                         select new GetProductCategoryForViewDto() {
							ProductCategory = new ProductCategoryDto
							{
                                Material = o.Material,
                                Extruder = o.Extruder,
                                Pipehead = o.Pipehead,
                                Id = o.Id
							}
						};

            var totalCount = await filteredProductCategories.CountAsync();

            return new PagedResultDto<GetProductCategoryForViewDto>(
                totalCount,
                await productCategories.ToListAsync()
            );
         }
		 
		 public async Task<GetProductCategoryForViewDto> GetProductCategoryForView(int id)
         {
            var productCategory = await _productCategoryRepository.GetAsync(id);

            var output = new GetProductCategoryForViewDto { ProductCategory = ObjectMapper.Map<ProductCategoryDto>(productCategory) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ProductCategories_Edit)]
		 public async Task<GetProductCategoryForEditOutput> GetProductCategoryForEdit(EntityDto input)
         {
            var productCategory = await _productCategoryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetProductCategoryForEditOutput {ProductCategory = ObjectMapper.Map<CreateOrEditProductCategoryDto>(productCategory)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditProductCategoryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ProductCategories_Create)]
		 private async Task Create(CreateOrEditProductCategoryDto input)
         {
            var productCategory = ObjectMapper.Map<ProductCategory>(input);

			

            await _productCategoryRepository.InsertAsync(productCategory);
         }

		 [AbpAuthorize(AppPermissions.Pages_ProductCategories_Edit)]
		 private async Task Update(CreateOrEditProductCategoryDto input)
         {
            var productCategory = await _productCategoryRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, productCategory);
         }

		 [AbpAuthorize(AppPermissions.Pages_ProductCategories_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _productCategoryRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetProductCategoriesToExcel(GetAllProductCategoriesForExcelInput input)
         {
			
			var filteredProductCategories = _productCategoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Material.Contains(input.Filter) || e.Extruder.Contains(input.Filter) || e.Pipehead.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter),  e => e.Material.ToLower() == input.MaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ExtruderFilter),  e => e.Extruder.ToLower() == input.ExtruderFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeheadFilter),  e => e.Pipehead.ToLower() == input.PipeheadFilter.ToLower().Trim());

			var query = (from o in filteredProductCategories
                         select new GetProductCategoryForViewDto() { 
							ProductCategory = new ProductCategoryDto
							{
                                Material = o.Material,
                                Extruder = o.Extruder,
                                Pipehead = o.Pipehead,
                                Id = o.Id
							}
						 });


            var productCategoryListDtos = await query.ToListAsync();

            return _productCategoriesExcelExporter.ExportToFile(productCategoryListDtos);
         }


    }
}