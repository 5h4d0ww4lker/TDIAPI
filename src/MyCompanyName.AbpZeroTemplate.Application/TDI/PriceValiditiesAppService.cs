

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
	[AbpAuthorize(AppPermissions.Pages_PriceValidities)]
    public class PriceValiditiesAppService : AbpZeroTemplateAppServiceBase, IPriceValiditiesAppService
    {
		 private readonly IRepository<PriceValidity> _priceValidityRepository;
		 private readonly IPriceValiditiesExcelExporter _priceValiditiesExcelExporter;
		 

		  public PriceValiditiesAppService(IRepository<PriceValidity> priceValidityRepository, IPriceValiditiesExcelExporter priceValiditiesExcelExporter ) 
		  {
			_priceValidityRepository = priceValidityRepository;
			_priceValiditiesExcelExporter = priceValiditiesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPriceValidityForViewDto>> GetAll(GetAllPriceValiditiesInput input)
         {
			
			var filteredPriceValidities = _priceValidityRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPriceValidities = filteredPriceValidities
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var priceValidities = from o in pagedAndFilteredPriceValidities
                         select new GetPriceValidityForViewDto() {
							PriceValidity = new PriceValidityDto
							{
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPriceValidities.CountAsync();

            return new PagedResultDto<GetPriceValidityForViewDto>(
                totalCount,
                await priceValidities.ToListAsync()
            );
         }
		 
		 public async Task<GetPriceValidityForViewDto> GetPriceValidityForView(int id)
         {
            var priceValidity = await _priceValidityRepository.GetAsync(id);

            var output = new GetPriceValidityForViewDto { PriceValidity = ObjectMapper.Map<PriceValidityDto>(priceValidity) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PriceValidities_Edit)]
		 public async Task<GetPriceValidityForEditOutput> GetPriceValidityForEdit(EntityDto input)
         {
            var priceValidity = await _priceValidityRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPriceValidityForEditOutput {PriceValidity = ObjectMapper.Map<CreateOrEditPriceValidityDto>(priceValidity)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPriceValidityDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PriceValidities_Create)]
		 private async Task Create(CreateOrEditPriceValidityDto input)
         {
            var priceValidity = ObjectMapper.Map<PriceValidity>(input);

			

            await _priceValidityRepository.InsertAsync(priceValidity);
         }

		 [AbpAuthorize(AppPermissions.Pages_PriceValidities_Edit)]
		 private async Task Update(CreateOrEditPriceValidityDto input)
         {
            var priceValidity = await _priceValidityRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, priceValidity);
         }

		 [AbpAuthorize(AppPermissions.Pages_PriceValidities_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _priceValidityRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPriceValiditiesToExcel(GetAllPriceValiditiesForExcelInput input)
         {
			
			var filteredPriceValidities = _priceValidityRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPriceValidities
                         select new GetPriceValidityForViewDto() { 
							PriceValidity = new PriceValidityDto
							{
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var priceValidityListDtos = await query.ToListAsync();

            return _priceValiditiesExcelExporter.ExportToFile(priceValidityListDtos);
         }


    }
}