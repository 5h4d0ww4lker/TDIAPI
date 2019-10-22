using MyCompanyName.AbpZeroTemplate.TDI;
using MyCompanyName.AbpZeroTemplate.TDI;
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
	[AbpAuthorize(AppPermissions.Pages_Quotations)]
    public class QuotationsAppService : AbpZeroTemplateAppServiceBase, IQuotationsAppService
    {
		 private readonly IRepository<Quotation> _quotationRepository;
		 private readonly IQuotationsExcelExporter _quotationsExcelExporter;
		 private readonly IRepository<Client,int> _lookup_clientRepository;
		 private readonly IRepository<ProductCategory,int> _lookup_productCategoryRepository;
		 private readonly IRepository<PaymentTerm,int> _lookup_paymentTermRepository;
		 private readonly IRepository<PriceValidity,int> _lookup_priceValidityRepository;
		 

		  public QuotationsAppService(IRepository<Quotation> quotationRepository, IQuotationsExcelExporter quotationsExcelExporter , IRepository<Client, int> lookup_clientRepository, IRepository<ProductCategory, int> lookup_productCategoryRepository, IRepository<PaymentTerm, int> lookup_paymentTermRepository, IRepository<PriceValidity, int> lookup_priceValidityRepository) 
		  {
			_quotationRepository = quotationRepository;
			_quotationsExcelExporter = quotationsExcelExporter;
			_lookup_clientRepository = lookup_clientRepository;
		_lookup_productCategoryRepository = lookup_productCategoryRepository;
		_lookup_paymentTermRepository = lookup_paymentTermRepository;
		_lookup_priceValidityRepository = lookup_priceValidityRepository;
		
		  }

		 public async Task<PagedResultDto<GetQuotationForViewDto>> GetAll(GetAllQuotationsInput input)
         {
			
			var filteredQuotations = _quotationRepository.GetAll()
						.Include( e => e.ClientFk)
						.Include( e => e.ProductCategoryFk)
						.Include( e => e.PaymentTermFk)
						.Include( e => e.PriceValidityFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.QuotationNumber.Contains(input.Filter) || e.ShipmentTypes.Contains(input.Filter) || e.DiscountInPercent.Contains(input.Filter) || e.DiscountInAmount.Contains(input.Filter) || e.PlaceOfDelivery.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.CheckedBy.Contains(input.Filter) || e.ApprovedBy.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationNumberFilter),  e => e.QuotationNumber.ToLower() == input.QuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ShipmentTypesFilter),  e => e.ShipmentTypes.ToLower() == input.ShipmentTypesFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInPercentFilter),  e => e.DiscountInPercent.ToLower() == input.DiscountInPercentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInAmountFilter),  e => e.DiscountInAmount.ToLower() == input.DiscountInAmountFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlaceOfDeliveryFilter),  e => e.PlaceOfDelivery.ToLower() == input.PlaceOfDeliveryFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CheckedByFilter),  e => e.CheckedBy.ToLower() == input.CheckedByFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ApprovedByFilter),  e => e.ApprovedBy.ToLower() == input.ApprovedByFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientClientNameFilter), e => e.ClientFk != null && e.ClientFk.ClientName.ToLower() == input.ClientClientNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PaymentTermDescriptionFilter), e => e.PaymentTermFk != null && e.PaymentTermFk.Description.ToLower() == input.PaymentTermDescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceValidityDescriptionFilter), e => e.PriceValidityFk != null && e.PriceValidityFk.Description.ToLower() == input.PriceValidityDescriptionFilter.ToLower().Trim());

			var pagedAndFilteredQuotations = filteredQuotations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var quotations = from o in pagedAndFilteredQuotations
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_paymentTermRepository.GetAll() on o.PaymentTermId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         join o4 in _lookup_priceValidityRepository.GetAll() on o.PriceValidityId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         
                         select new GetQuotationForViewDto() {
							Quotation = new QuotationDto
							{
                                QuotationNumber = o.QuotationNumber,
                                ShipmentTypes = o.ShipmentTypes,
                                DiscountInPercent = o.DiscountInPercent,
                                DiscountInAmount = o.DiscountInAmount,
                                PlaceOfDelivery = o.PlaceOfDelivery,
                                Status = o.Status,
                                CheckedBy = o.CheckedBy,
                                ApprovedBy = o.ApprovedBy,
                                Id = o.Id
							},
                         	ClientClientName = s1 == null ? "" : s1.ClientName.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString(),
                         	PaymentTermDescription = s3 == null ? "" : s3.Description.ToString(),
                         	PriceValidityDescription = s4 == null ? "" : s4.Description.ToString()
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

		    if (output.Quotation.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.Quotation.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }

		    if (output.Quotation.PaymentTermId != null)
            {
                var _lookupPaymentTerm = await _lookup_paymentTermRepository.FirstOrDefaultAsync((int)output.Quotation.PaymentTermId);
                output.PaymentTermDescription = _lookupPaymentTerm.Description.ToString();
            }

		    if (output.Quotation.PriceValidityId != null)
            {
                var _lookupPriceValidity = await _lookup_priceValidityRepository.FirstOrDefaultAsync((int)output.Quotation.PriceValidityId);
                output.PriceValidityDescription = _lookupPriceValidity.Description.ToString();
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

		    if (output.Quotation.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.Quotation.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }

		    if (output.Quotation.PaymentTermId != null)
            {
                var _lookupPaymentTerm = await _lookup_paymentTermRepository.FirstOrDefaultAsync((int)output.Quotation.PaymentTermId);
                output.PaymentTermDescription = _lookupPaymentTerm.Description.ToString();
            }

		    if (output.Quotation.PriceValidityId != null)
            {
                var _lookupPriceValidity = await _lookup_priceValidityRepository.FirstOrDefaultAsync((int)output.Quotation.PriceValidityId);
                output.PriceValidityDescription = _lookupPriceValidity.Description.ToString();
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
						.Include( e => e.ProductCategoryFk)
						.Include( e => e.PaymentTermFk)
						.Include( e => e.PriceValidityFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.QuotationNumber.Contains(input.Filter) || e.ShipmentTypes.Contains(input.Filter) || e.DiscountInPercent.Contains(input.Filter) || e.DiscountInAmount.Contains(input.Filter) || e.PlaceOfDelivery.Contains(input.Filter) || e.Status.Contains(input.Filter) || e.CheckedBy.Contains(input.Filter) || e.ApprovedBy.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationNumberFilter),  e => e.QuotationNumber.ToLower() == input.QuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ShipmentTypesFilter),  e => e.ShipmentTypes.ToLower() == input.ShipmentTypesFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInPercentFilter),  e => e.DiscountInPercent.ToLower() == input.DiscountInPercentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DiscountInAmountFilter),  e => e.DiscountInAmount.ToLower() == input.DiscountInAmountFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlaceOfDeliveryFilter),  e => e.PlaceOfDelivery.ToLower() == input.PlaceOfDeliveryFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter),  e => e.Status.ToLower() == input.StatusFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CheckedByFilter),  e => e.CheckedBy.ToLower() == input.CheckedByFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ApprovedByFilter),  e => e.ApprovedBy.ToLower() == input.ApprovedByFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientClientNameFilter), e => e.ClientFk != null && e.ClientFk.ClientName.ToLower() == input.ClientClientNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PaymentTermDescriptionFilter), e => e.PaymentTermFk != null && e.PaymentTermFk.Description.ToLower() == input.PaymentTermDescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PriceValidityDescriptionFilter), e => e.PriceValidityFk != null && e.PriceValidityFk.Description.ToLower() == input.PriceValidityDescriptionFilter.ToLower().Trim());

			var query = (from o in filteredQuotations
                         join o1 in _lookup_clientRepository.GetAll() on o.ClientId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_paymentTermRepository.GetAll() on o.PaymentTermId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         join o4 in _lookup_priceValidityRepository.GetAll() on o.PriceValidityId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         
                         select new GetQuotationForViewDto() { 
							Quotation = new QuotationDto
							{
                                QuotationNumber = o.QuotationNumber,
                                ShipmentTypes = o.ShipmentTypes,
                                DiscountInPercent = o.DiscountInPercent,
                                DiscountInAmount = o.DiscountInAmount,
                                PlaceOfDelivery = o.PlaceOfDelivery,
                                Status = o.Status,
                                CheckedBy = o.CheckedBy,
                                ApprovedBy = o.ApprovedBy,
                                Id = o.Id
							},
                         	ClientClientName = s1 == null ? "" : s1.ClientName.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString(),
                         	PaymentTermDescription = s3 == null ? "" : s3.Description.ToString(),
                         	PriceValidityDescription = s4 == null ? "" : s4.Description.ToString()
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

		[AbpAuthorize(AppPermissions.Pages_Quotations)]
         public async Task<PagedResultDto<QuotationProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_productCategoryRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Material.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var productCategoryList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationProductCategoryLookupTableDto>();
			foreach(var productCategory in productCategoryList){
				lookupTableDtoList.Add(new QuotationProductCategoryLookupTableDto
				{
					Id = productCategory.Id,
					DisplayName = productCategory.Material?.ToString()
				});
			}

            return new PagedResultDto<QuotationProductCategoryLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Quotations)]
         public async Task<PagedResultDto<QuotationPaymentTermLookupTableDto>> GetAllPaymentTermForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_paymentTermRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Description.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var paymentTermList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationPaymentTermLookupTableDto>();
			foreach(var paymentTerm in paymentTermList){
				lookupTableDtoList.Add(new QuotationPaymentTermLookupTableDto
				{
					Id = paymentTerm.Id,
					DisplayName = paymentTerm.Description?.ToString()
				});
			}

            return new PagedResultDto<QuotationPaymentTermLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Quotations)]
         public async Task<PagedResultDto<QuotationPriceValidityLookupTableDto>> GetAllPriceValidityForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_priceValidityRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Description.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var priceValidityList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationPriceValidityLookupTableDto>();
			foreach(var priceValidity in priceValidityList){
				lookupTableDtoList.Add(new QuotationPriceValidityLookupTableDto
				{
					Id = priceValidity.Id,
					DisplayName = priceValidity.Description?.ToString()
				});
			}

            return new PagedResultDto<QuotationPriceValidityLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}