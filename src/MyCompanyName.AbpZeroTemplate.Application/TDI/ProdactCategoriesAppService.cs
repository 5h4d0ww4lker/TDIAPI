

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
	[AbpAuthorize(AppPermissions.Pages_ProdactCategories)]
    public class ProdactCategoriesAppService : AbpZeroTemplateAppServiceBase, IProdactCategoriesAppService
    {
		 private readonly IRepository<ProdactCategory> _prodactCategoryRepository;
		 private readonly IProdactCategoriesExcelExporter _prodactCategoriesExcelExporter;
		 

		  public ProdactCategoriesAppService(IRepository<ProdactCategory> prodactCategoryRepository, IProdactCategoriesExcelExporter prodactCategoriesExcelExporter ) 
		  {
			_prodactCategoryRepository = prodactCategoryRepository;
			_prodactCategoriesExcelExporter = prodactCategoriesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetProdactCategoryForViewDto>> GetAll(GetAllProdactCategoriesInput input)
         {
			
			var filteredProdactCategories = _prodactCategoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Material.Contains(input.Filter) || e.Extruder.Contains(input.Filter) || e.Pipehead.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter),  e => e.Material.ToLower() == input.MaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ExtruderFilter),  e => e.Extruder.ToLower() == input.ExtruderFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeheadFilter),  e => e.Pipehead.ToLower() == input.PipeheadFilter.ToLower().Trim());

			var pagedAndFilteredProdactCategories = filteredProdactCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var prodactCategories = from o in pagedAndFilteredProdactCategories
                         select new GetProdactCategoryForViewDto() {
							ProdactCategory = new ProdactCategoryDto
							{
                                Material = o.Material,
                                Extruder = o.Extruder,
                                Pipehead = o.Pipehead,
                                Id = o.Id
							}
						};

            var totalCount = await filteredProdactCategories.CountAsync();

            return new PagedResultDto<GetProdactCategoryForViewDto>(
                totalCount,
                await prodactCategories.ToListAsync()
            );
         }
		 
		 public async Task<GetProdactCategoryForViewDto> GetProdactCategoryForView(int id)
         {
            var prodactCategory = await _prodactCategoryRepository.GetAsync(id);

            var output = new GetProdactCategoryForViewDto { ProdactCategory = ObjectMapper.Map<ProdactCategoryDto>(prodactCategory) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ProdactCategories_Edit)]
		 public async Task<GetProdactCategoryForEditOutput> GetProdactCategoryForEdit(EntityDto input)
         {
            var prodactCategory = await _prodactCategoryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetProdactCategoryForEditOutput {ProdactCategory = ObjectMapper.Map<CreateOrEditProdactCategoryDto>(prodactCategory)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditProdactCategoryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ProdactCategories_Create)]
		 private async Task Create(CreateOrEditProdactCategoryDto input)
         {
            var prodactCategory = ObjectMapper.Map<ProdactCategory>(input);

			

            await _prodactCategoryRepository.InsertAsync(prodactCategory);
         }

		 [AbpAuthorize(AppPermissions.Pages_ProdactCategories_Edit)]
		 private async Task Update(CreateOrEditProdactCategoryDto input)
         {
            var prodactCategory = await _prodactCategoryRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, prodactCategory);
         }

		 [AbpAuthorize(AppPermissions.Pages_ProdactCategories_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _prodactCategoryRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetProdactCategoriesToExcel(GetAllProdactCategoriesForExcelInput input)
         {
			
			var filteredProdactCategories = _prodactCategoryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Material.Contains(input.Filter) || e.Extruder.Contains(input.Filter) || e.Pipehead.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.MaterialFilter),  e => e.Material.ToLower() == input.MaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ExtruderFilter),  e => e.Extruder.ToLower() == input.ExtruderFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PipeheadFilter),  e => e.Pipehead.ToLower() == input.PipeheadFilter.ToLower().Trim());

			var query = (from o in filteredProdactCategories
                         select new GetProdactCategoryForViewDto() { 
							ProdactCategory = new ProdactCategoryDto
							{
                                Material = o.Material,
                                Extruder = o.Extruder,
                                Pipehead = o.Pipehead,
                                Id = o.Id
							}
						 });


            var prodactCategoryListDtos = await query.ToListAsync();

            return _prodactCategoriesExcelExporter.ExportToFile(prodactCategoryListDtos);
         }


    }
}