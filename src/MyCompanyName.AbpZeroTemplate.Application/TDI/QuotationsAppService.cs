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
	[AbpAuthorize(AppPermissions.Pages_Quotations)]
    public class QuotationsAppService : AbpZeroTemplateAppServiceBase, IQuotationsAppService
    {
		 private readonly IRepository<Quotation> _quotationRepository;
		 private readonly IQuotationsExcelExporter _quotationsExcelExporter;
		 private readonly IRepository<Client,int> _lookup_clientRepository;
		 

		  public QuotationsAppService(IRepository<Quotation> quotationRepository, IQuotationsExcelExporter quotationsExcelExporter , IRepository<Client, int> lookup_clientRepository) 
		  {
			_quotationRepository = quotationRepository;
			_quotationsExcelExporter = quotationsExcelExporter;
			_lookup_clientRepository = lookup_clientRepository;
		
		  }

		 public async Task<PagedResultDto<GetQuotationForViewDto>> GetAll(GetAllQuotationsInput input)
         {
			
			var filteredQuotations = _quotationRepository.GetAll()
						.Include( e => e.ClientFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.QuotationNumber.Contains(input.Filter) || e.PriceValidity.Contains(input.Filter) || e.TermOfPayment.Contains(input.Filter) || e.ShipmentTypes.Contains(input.Filter) || e.DiscountInPercent.Contains(input.Filter) || e.DiscountInAmount.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationNumberFilter),  e => e.QuotationNumber.ToLower() == input.QuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceValidityFilter),  e => e.PriceValidity.ToLower() == input.PriceValidityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TermOfPaymentFilter),  e => e.TermOfPayment.ToLower() == input.TermOfPaymentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ShipmentTypesFilter),  e => e.ShipmentTypes.ToLower() == input.ShipmentTypesFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInPercentFilter),  e => e.DiscountInPercent.ToLower() == input.DiscountInPercentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInAmountFilter),  e => e.DiscountInAmount.ToLower() == input.DiscountInAmountFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientClientNameFilter), e => e.ClientFk != null && e.ClientFk.ClientName.ToLower() == input.ClientClientNameFilter.ToLower().Trim());

			var pagedAndFilteredQuotations = filteredQuotations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var quotations = from o in pagedAndFilteredQuotations
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetQuotationForViewDto() {
							Quotation = new QuotationDto
							{
                                QuotationNumber = o.QuotationNumber,
                                PriceValidity = o.PriceValidity,
                                TermOfPayment = o.TermOfPayment,
                                ShipmentTypes = o.ShipmentTypes,
                                DiscountInPercent = o.DiscountInPercent,
                                DiscountInAmount = o.DiscountInAmount,
                                Id = o.Id
							},
                         	ClientClientName = s1 == null ? "" : s1.ClientName.ToString()
						};

            var totalCount = await filteredQuotations.CountAsync();

            return new PagedResultDto<GetQuotationForViewDto>(
                totalCount,
                await quotations.ToListAsync()
            );
         }
		 
		 public async Task<GetQuotationForViewDto> GetQuotationForView(int id)
         {
            var quotation = await _quotationRepository.GetAsync(id);

            var output = new GetQuotationForViewDto { Quotation = ObjectMapper.Map<QuotationDto>(quotation) };

		    if (output.Quotation.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((int)output.Quotation.ClientId);
                output.ClientClientName = _lookupClient.ClientName.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Quotations_Edit)]
		 public async Task<GetQuotationForEditOutput> GetQuotationForEdit(EntityDto input)
         {
            var quotation = await _quotationRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetQuotationForEditOutput {Quotation = ObjectMapper.Map<CreateOrEditQuotationDto>(quotation)};

		    if (output.Quotation.ClientId != null)
            {
                var _lookupClient = await _lookup_clientRepository.FirstOrDefaultAsync((int)output.Quotation.ClientId);
                output.ClientClientName = _lookupClient.ClientName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditQuotationDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Quotations_Create)]
		 private async Task Create(CreateOrEditQuotationDto input)
         {
            var quotation = ObjectMapper.Map<Quotation>(input);

			

            await _quotationRepository.InsertAsync(quotation);
         }

		 [AbpAuthorize(AppPermissions.Pages_Quotations_Edit)]
		 private async Task Update(CreateOrEditQuotationDto input)
         {
            var quotation = await _quotationRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, quotation);
         }

		 [AbpAuthorize(AppPermissions.Pages_Quotations_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _quotationRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetQuotationsToExcel(GetAllQuotationsForExcelInput input)
         {
			
			var filteredQuotations = _quotationRepository.GetAll()
						.Include( e => e.ClientFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.QuotationNumber.Contains(input.Filter) || e.PriceValidity.Contains(input.Filter) || e.TermOfPayment.Contains(input.Filter) || e.ShipmentTypes.Contains(input.Filter) || e.DiscountInPercent.Contains(input.Filter) || e.DiscountInAmount.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationNumberFilter),  e => e.QuotationNumber.ToLower() == input.QuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceValidityFilter),  e => e.PriceValidity.ToLower() == input.PriceValidityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TermOfPaymentFilter),  e => e.TermOfPayment.ToLower() == input.TermOfPaymentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ShipmentTypesFilter),  e => e.ShipmentTypes.ToLower() == input.ShipmentTypesFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInPercentFilter),  e => e.DiscountInPercent.ToLower() == input.DiscountInPercentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInAmountFilter),  e => e.DiscountInAmount.ToLower() == input.DiscountInAmountFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientClientNameFilter), e => e.ClientFk != null && e.ClientFk.ClientName.ToLower() == input.ClientClientNameFilter.ToLower().Trim());

			var query = (from o in filteredQuotations
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetQuotationForViewDto() { 
							Quotation = new QuotationDto
							{
                                QuotationNumber = o.QuotationNumber,
                                PriceValidity = o.PriceValidity,
                                TermOfPayment = o.TermOfPayment,
                                ShipmentTypes = o.ShipmentTypes,
                                DiscountInPercent = o.DiscountInPercent,
                                DiscountInAmount = o.DiscountInAmount,
                                Id = o.Id
							},
                         	ClientClientName = s1 == null ? "" : s1.ClientName.ToString()
						 });


            var quotationListDtos = await query.ToListAsync();

            return _quotationsExcelExporter.ExportToFile(quotationListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Quotations)]
         public async Task<PagedResultDto<QuotationClientLookupTableDto>> GetAllClientForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_clientRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ClientName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var clientList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationClientLookupTableDto>();
			foreach(var client in clientList){
				lookupTableDtoList.Add(new QuotationClientLookupTableDto
				{
					Id = client.Id,
					DisplayName = client.ClientName?.ToString()
				});
			}

            return new PagedResultDto<QuotationClientLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}