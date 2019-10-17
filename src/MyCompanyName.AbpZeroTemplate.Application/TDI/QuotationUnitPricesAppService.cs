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
	[AbpAuthorize(AppPermissions.Pages_QuotationUnitPrices)]
    public class QuotationUnitPricesAppService : AbpZeroTemplateAppServiceBase, IQuotationUnitPricesAppService
    {
		 private readonly IRepository<QuotationUnitPrice> _quotationUnitPriceRepository;
		 private readonly IQuotationUnitPricesExcelExporter _quotationUnitPricesExcelExporter;
		 private readonly IRepository<Quotation,int> _lookup_quotationRepository;
		 

		  public QuotationUnitPricesAppService(IRepository<QuotationUnitPrice> quotationUnitPriceRepository, IQuotationUnitPricesExcelExporter quotationUnitPricesExcelExporter , IRepository<Quotation, int> lookup_quotationRepository) 
		  {
			_quotationUnitPriceRepository = quotationUnitPriceRepository;
			_quotationUnitPricesExcelExporter = quotationUnitPricesExcelExporter;
			_lookup_quotationRepository = lookup_quotationRepository;
		
		  }

		 public async Task<PagedResultDto<GetQuotationUnitPriceForViewDto>> GetAll(GetAllQuotationUnitPricesInput input)
         {
			
			var filteredQuotationUnitPrices = _quotationUnitPriceRepository.GetAll()
						.Include( e => e.QuotationFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter) || e.Unit.Contains(input.Filter) || e.Price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitFilter),  e => e.Unit.ToLower() == input.UnitFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationQuotationNumberFilter), e => e.QuotationFk != null && e.QuotationFk.QuotationNumber.ToLower() == input.QuotationQuotationNumberFilter.ToLower().Trim());

			var pagedAndFilteredQuotationUnitPrices = filteredQuotationUnitPrices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var quotationUnitPrices = from o in pagedAndFilteredQuotationUnitPrices
                         join o1 in _lookup_quotationRepository.GetAll() on o.QuotationId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetQuotationUnitPriceForViewDto() {
							QuotationUnitPrice = new QuotationUnitPriceDto
							{
                                Description = o.Description,
                                Unit = o.Unit,
                                Price = o.Price,
                                Id = o.Id
							},
                         	QuotationQuotationNumber = s1 == null ? "" : s1.QuotationNumber.ToString()
						};

            var totalCount = await filteredQuotationUnitPrices.CountAsync();

            return new PagedResultDto<GetQuotationUnitPriceForViewDto>(
                totalCount,
                await quotationUnitPrices.ToListAsync()
            );
         }
		 
		 public async Task<GetQuotationUnitPriceForViewDto> GetQuotationUnitPriceForView(int id)
         {
            var quotationUnitPrice = await _quotationUnitPriceRepository.GetAsync(id);

            var output = new GetQuotationUnitPriceForViewDto { QuotationUnitPrice = ObjectMapper.Map<QuotationUnitPriceDto>(quotationUnitPrice) };

		    if (output.QuotationUnitPrice.QuotationId != null)
            {
                var _lookupQuotation = await _lookup_quotationRepository.FirstOrDefaultAsync((int)output.QuotationUnitPrice.QuotationId);
                output.QuotationQuotationNumber = _lookupQuotation.QuotationNumber.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_QuotationUnitPrices_Edit)]
		 public async Task<GetQuotationUnitPriceForEditOutput> GetQuotationUnitPriceForEdit(EntityDto input)
         {
            var quotationUnitPrice = await _quotationUnitPriceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetQuotationUnitPriceForEditOutput {QuotationUnitPrice = ObjectMapper.Map<CreateOrEditQuotationUnitPriceDto>(quotationUnitPrice)};

		    if (output.QuotationUnitPrice.QuotationId != null)
            {
                var _lookupQuotation = await _lookup_quotationRepository.FirstOrDefaultAsync((int)output.QuotationUnitPrice.QuotationId);
                output.QuotationQuotationNumber = _lookupQuotation.QuotationNumber.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditQuotationUnitPriceDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_QuotationUnitPrices_Create)]
		 private async Task Create(CreateOrEditQuotationUnitPriceDto input)
         {
            var quotationUnitPrice = ObjectMapper.Map<QuotationUnitPrice>(input);

			

            await _quotationUnitPriceRepository.InsertAsync(quotationUnitPrice);
         }

		 [AbpAuthorize(AppPermissions.Pages_QuotationUnitPrices_Edit)]
		 private async Task Update(CreateOrEditQuotationUnitPriceDto input)
         {
            var quotationUnitPrice = await _quotationUnitPriceRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, quotationUnitPrice);
         }

		 [AbpAuthorize(AppPermissions.Pages_QuotationUnitPrices_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _quotationUnitPriceRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetQuotationUnitPricesToExcel(GetAllQuotationUnitPricesForExcelInput input)
         {
			
			var filteredQuotationUnitPrices = _quotationUnitPriceRepository.GetAll()
						.Include( e => e.QuotationFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Description.Contains(input.Filter) || e.Unit.Contains(input.Filter) || e.Price.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitFilter),  e => e.Unit.ToLower() == input.UnitFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceFilter),  e => e.Price.ToLower() == input.PriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationQuotationNumberFilter), e => e.QuotationFk != null && e.QuotationFk.QuotationNumber.ToLower() == input.QuotationQuotationNumberFilter.ToLower().Trim());

			var query = (from o in filteredQuotationUnitPrices
                         join o1 in _lookup_quotationRepository.GetAll() on o.QuotationId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetQuotationUnitPriceForViewDto() { 
							QuotationUnitPrice = new QuotationUnitPriceDto
							{
                                Description = o.Description,
                                Unit = o.Unit,
                                Price = o.Price,
                                Id = o.Id
							},
                         	QuotationQuotationNumber = s1 == null ? "" : s1.QuotationNumber.ToString()
						 });


            var quotationUnitPriceListDtos = await query.ToListAsync();

            return _quotationUnitPricesExcelExporter.ExportToFile(quotationUnitPriceListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_QuotationUnitPrices)]
         public async Task<PagedResultDto<QuotationUnitPriceQuotationLookupTableDto>> GetAllQuotationForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_quotationRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.QuotationNumber.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var quotationList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationUnitPriceQuotationLookupTableDto>();
			foreach(var quotation in quotationList){
				lookupTableDtoList.Add(new QuotationUnitPriceQuotationLookupTableDto
				{
					Id = quotation.Id,
					DisplayName = quotation.QuotationNumber?.ToString()
				});
			}

            return new PagedResultDto<QuotationUnitPriceQuotationLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}