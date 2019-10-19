using MyCompanyName.AbpZeroTemplate.TDI;
using MyCompanyName.AbpZeroTemplate.TDI;
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
	[AbpAuthorize(AppPermissions.Pages_QuotationItems)]
    public class QuotationItemsAppService : AbpZeroTemplateAppServiceBase, IQuotationItemsAppService
    {
		 private readonly IRepository<QuotationItem> _quotationItemRepository;
		 private readonly IQuotationItemsExcelExporter _quotationItemsExcelExporter;
		 private readonly IRepository<Quotation,int> _lookup_quotationRepository;
		 private readonly IRepository<ProductCategory,int> _lookup_productCategoryRepository;
		 private readonly IRepository<ProductSubCategory,int> _lookup_productSubCategoryRepository;
		 private readonly IRepository<UnitPrice,int> _lookup_unitPriceRepository;
		 private readonly IRepository<ClientUnitPrice,int> _lookup_clientUnitPriceRepository;
		 private readonly IRepository<QuotationUnitPrice,int> _lookup_quotationUnitPriceRepository;
		 

		  public QuotationItemsAppService(IRepository<QuotationItem> quotationItemRepository, IQuotationItemsExcelExporter quotationItemsExcelExporter , IRepository<Quotation, int> lookup_quotationRepository, IRepository<ProductCategory, int> lookup_productCategoryRepository, IRepository<ProductSubCategory, int> lookup_productSubCategoryRepository, IRepository<UnitPrice, int> lookup_unitPriceRepository, IRepository<ClientUnitPrice, int> lookup_clientUnitPriceRepository, IRepository<QuotationUnitPrice, int> lookup_quotationUnitPriceRepository) 
		  {
			_quotationItemRepository = quotationItemRepository;
			_quotationItemsExcelExporter = quotationItemsExcelExporter;
			_lookup_quotationRepository = lookup_quotationRepository;
		_lookup_productCategoryRepository = lookup_productCategoryRepository;
		_lookup_productSubCategoryRepository = lookup_productSubCategoryRepository;
		_lookup_unitPriceRepository = lookup_unitPriceRepository;
		_lookup_clientUnitPriceRepository = lookup_clientUnitPriceRepository;
		_lookup_quotationUnitPriceRepository = lookup_quotationUnitPriceRepository;
		
		  }

		 public async Task<PagedResultDto<GetQuotationItemForViewDto>> GetAll(GetAllQuotationItemsInput input)
         {
			
			var filteredQuotationItems = _quotationItemRepository.GetAll()
						.Include( e => e.QuotationFk)
						.Include( e => e.ProductCategoryFk)
						.Include( e => e.ProductSubCategoryFk)
						.Include( e => e.UnitPriceFk)
						.Include( e => e.ClientUnitPriceFk)
						.Include( e => e.QuotationUnitPriceFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Quantity.Contains(input.Filter) || e.TotalAmountInETB.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuantityFilter),  e => e.Quantity.ToLower() == input.QuantityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TotalAmountInETBFilter),  e => e.TotalAmountInETB.ToLower() == input.TotalAmountInETBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationQuotationNumberFilter), e => e.QuotationFk != null && e.QuotationFk.QuotationNumber.ToLower() == input.QuotationQuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductSubCategoryPipeDiameterFilter), e => e.ProductSubCategoryFk != null && e.ProductSubCategoryFk.PipeDiameter.ToLower() == input.ProductSubCategoryPipeDiameterFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitPricePriceFilter), e => e.UnitPriceFk != null && e.UnitPriceFk.Price.ToLower() == input.UnitPricePriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientUnitPriceDescriptionFilter), e => e.ClientUnitPriceFk != null && e.ClientUnitPriceFk.Description.ToLower() == input.ClientUnitPriceDescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationUnitPriceDescriptionFilter), e => e.QuotationUnitPriceFk != null && e.QuotationUnitPriceFk.Description.ToLower() == input.QuotationUnitPriceDescriptionFilter.ToLower().Trim());

			var pagedAndFilteredQuotationItems = filteredQuotationItems
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var quotationItems = from o in pagedAndFilteredQuotationItems
                         join o1 in _lookup_quotationRepository.GetAll() on o.QuotationId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_productSubCategoryRepository.GetAll() on o.ProductSubCategoryId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         join o4 in _lookup_unitPriceRepository.GetAll() on o.UnitPriceId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         
                         join o5 in _lookup_clientUnitPriceRepository.GetAll() on o.ClientUnitPriceId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()
                         
                         join o6 in _lookup_quotationUnitPriceRepository.GetAll() on o.QuotationUnitPriceId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()
                         
                         select new GetQuotationItemForViewDto() {
							QuotationItem = new QuotationItemDto
							{
                                Quantity = o.Quantity,
                                TotalAmountInETB = o.TotalAmountInETB,
                                Description = o.Description,
                                Id = o.Id
							},
                         	QuotationQuotationNumber = s1 == null ? "" : s1.QuotationNumber.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString(),
                         	ProductSubCategoryPipeDiameter = s3 == null ? "" : s3.PipeDiameter.ToString(),
                         	UnitPricePrice = s4 == null ? "" : s4.Price.ToString(),
                         	ClientUnitPriceDescription = s5 == null ? "" : s5.Description.ToString(),
                         	QuotationUnitPriceDescription = s6 == null ? "" : s6.Description.ToString()
						};

            var totalCount = await filteredQuotationItems.CountAsync();

            return new PagedResultDto<GetQuotationItemForViewDto>(
                totalCount,
                await quotationItems.ToListAsync()
            );
         }
		 
		 public async Task<GetQuotationItemForViewDto> GetQuotationItemForView(int id)
         {
            var quotationItem = await _quotationItemRepository.GetAsync(id);

            var output = new GetQuotationItemForViewDto { QuotationItem = ObjectMapper.Map<QuotationItemDto>(quotationItem) };

		    if (output.QuotationItem.QuotationId != null)
            {
                var _lookupQuotation = await _lookup_quotationRepository.FirstOrDefaultAsync((int)output.QuotationItem.QuotationId);
                output.QuotationQuotationNumber = _lookupQuotation.QuotationNumber.ToString();
            }

		    if (output.QuotationItem.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.QuotationItem.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }

		    if (output.QuotationItem.ProductSubCategoryId != null)
            {
                var _lookupProductSubCategory = await _lookup_productSubCategoryRepository.FirstOrDefaultAsync((int)output.QuotationItem.ProductSubCategoryId);
                output.ProductSubCategoryPipeDiameter = _lookupProductSubCategory.PipeDiameter.ToString();
            }

		    if (output.QuotationItem.UnitPriceId != null)
            {
                var _lookupUnitPrice = await _lookup_unitPriceRepository.FirstOrDefaultAsync((int)output.QuotationItem.UnitPriceId);
                output.UnitPricePrice = _lookupUnitPrice.Price.ToString();
            }

		    if (output.QuotationItem.ClientUnitPriceId != null)
            {
                var _lookupClientUnitPrice = await _lookup_clientUnitPriceRepository.FirstOrDefaultAsync((int)output.QuotationItem.ClientUnitPriceId);
                output.ClientUnitPriceDescription = _lookupClientUnitPrice.Description.ToString();
            }

		    if (output.QuotationItem.QuotationUnitPriceId != null)
            {
                var _lookupQuotationUnitPrice = await _lookup_quotationUnitPriceRepository.FirstOrDefaultAsync((int)output.QuotationItem.QuotationUnitPriceId);
                output.QuotationUnitPriceDescription = _lookupQuotationUnitPrice.Description.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_QuotationItems_Edit)]
		 public async Task<GetQuotationItemForEditOutput> GetQuotationItemForEdit(EntityDto input)
         {
            var quotationItem = await _quotationItemRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetQuotationItemForEditOutput {QuotationItem = ObjectMapper.Map<CreateOrEditQuotationItemDto>(quotationItem)};

		    if (output.QuotationItem.QuotationId != null)
            {
                var _lookupQuotation = await _lookup_quotationRepository.FirstOrDefaultAsync((int)output.QuotationItem.QuotationId);
                output.QuotationQuotationNumber = _lookupQuotation.QuotationNumber.ToString();
            }

		    if (output.QuotationItem.ProductCategoryId != null)
            {
                var _lookupProductCategory = await _lookup_productCategoryRepository.FirstOrDefaultAsync((int)output.QuotationItem.ProductCategoryId);
                output.ProductCategoryMaterial = _lookupProductCategory.Material.ToString();
            }

		    if (output.QuotationItem.ProductSubCategoryId != null)
            {
                var _lookupProductSubCategory = await _lookup_productSubCategoryRepository.FirstOrDefaultAsync((int)output.QuotationItem.ProductSubCategoryId);
                output.ProductSubCategoryPipeDiameter = _lookupProductSubCategory.PipeDiameter.ToString();
            }

		    if (output.QuotationItem.UnitPriceId != null)
            {
                var _lookupUnitPrice = await _lookup_unitPriceRepository.FirstOrDefaultAsync((int)output.QuotationItem.UnitPriceId);
                output.UnitPricePrice = _lookupUnitPrice.Price.ToString();
            }

		    if (output.QuotationItem.ClientUnitPriceId != null)
            {
                var _lookupClientUnitPrice = await _lookup_clientUnitPriceRepository.FirstOrDefaultAsync((int)output.QuotationItem.ClientUnitPriceId);
                output.ClientUnitPriceDescription = _lookupClientUnitPrice.Description.ToString();
            }

		    if (output.QuotationItem.QuotationUnitPriceId != null)
            {
                var _lookupQuotationUnitPrice = await _lookup_quotationUnitPriceRepository.FirstOrDefaultAsync((int)output.QuotationItem.QuotationUnitPriceId);
                output.QuotationUnitPriceDescription = _lookupQuotationUnitPrice.Description.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditQuotationItemDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_QuotationItems_Create)]
		 private async Task Create(CreateOrEditQuotationItemDto input)
         {
            var quotationItem = ObjectMapper.Map<QuotationItem>(input);

			

            await _quotationItemRepository.InsertAsync(quotationItem);
         }

		 [AbpAuthorize(AppPermissions.Pages_QuotationItems_Edit)]
		 private async Task Update(CreateOrEditQuotationItemDto input)
         {
            var quotationItem = await _quotationItemRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, quotationItem);
         }

		 [AbpAuthorize(AppPermissions.Pages_QuotationItems_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _quotationItemRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetQuotationItemsToExcel(GetAllQuotationItemsForExcelInput input)
         {
			
			var filteredQuotationItems = _quotationItemRepository.GetAll()
						.Include( e => e.QuotationFk)
						.Include( e => e.ProductCategoryFk)
						.Include( e => e.ProductSubCategoryFk)
						.Include( e => e.UnitPriceFk)
						.Include( e => e.ClientUnitPriceFk)
						.Include( e => e.QuotationUnitPriceFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Quantity.Contains(input.Filter) || e.TotalAmountInETB.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuantityFilter),  e => e.Quantity.ToLower() == input.QuantityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TotalAmountInETBFilter),  e => e.TotalAmountInETB.ToLower() == input.TotalAmountInETBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationQuotationNumberFilter), e => e.QuotationFk != null && e.QuotationFk.QuotationNumber.ToLower() == input.QuotationQuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductSubCategoryPipeDiameterFilter), e => e.ProductSubCategoryFk != null && e.ProductSubCategoryFk.PipeDiameter.ToLower() == input.ProductSubCategoryPipeDiameterFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UnitPricePriceFilter), e => e.UnitPriceFk != null && e.UnitPriceFk.Price.ToLower() == input.UnitPricePriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClientUnitPriceDescriptionFilter), e => e.ClientUnitPriceFk != null && e.ClientUnitPriceFk.Description.ToLower() == input.ClientUnitPriceDescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationUnitPriceDescriptionFilter), e => e.QuotationUnitPriceFk != null && e.QuotationUnitPriceFk.Description.ToLower() == input.QuotationUnitPriceDescriptionFilter.ToLower().Trim());

			var query = (from o in filteredQuotationItems
                         join o1 in _lookup_quotationRepository.GetAll() on o.QuotationId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_productSubCategoryRepository.GetAll() on o.ProductSubCategoryId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         join o4 in _lookup_unitPriceRepository.GetAll() on o.UnitPriceId equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()
                         
                         join o5 in _lookup_clientUnitPriceRepository.GetAll() on o.ClientUnitPriceId equals o5.Id into j5
                         from s5 in j5.DefaultIfEmpty()
                         
                         join o6 in _lookup_quotationUnitPriceRepository.GetAll() on o.QuotationUnitPriceId equals o6.Id into j6
                         from s6 in j6.DefaultIfEmpty()
                         
                         select new GetQuotationItemForViewDto() { 
							QuotationItem = new QuotationItemDto
							{
                                Quantity = o.Quantity,
                                TotalAmountInETB = o.TotalAmountInETB,
                                Description = o.Description,
                                Id = o.Id
							},
                         	QuotationQuotationNumber = s1 == null ? "" : s1.QuotationNumber.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString(),
                         	ProductSubCategoryPipeDiameter = s3 == null ? "" : s3.PipeDiameter.ToString(),
                         	UnitPricePrice = s4 == null ? "" : s4.Price.ToString(),
                         	ClientUnitPriceDescription = s5 == null ? "" : s5.Description.ToString(),
                         	QuotationUnitPriceDescription = s6 == null ? "" : s6.Description.ToString()
						 });


            var quotationItemListDtos = await query.ToListAsync();

            return _quotationItemsExcelExporter.ExportToFile(quotationItemListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_QuotationItems)]
         public async Task<PagedResultDto<QuotationItemQuotationLookupTableDto>> GetAllQuotationForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_quotationRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.QuotationNumber.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var quotationList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationItemQuotationLookupTableDto>();
			foreach(var quotation in quotationList){
				lookupTableDtoList.Add(new QuotationItemQuotationLookupTableDto
				{
					Id = quotation.Id,
					DisplayName = quotation.QuotationNumber?.ToString()
				});
			}

            return new PagedResultDto<QuotationItemQuotationLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_QuotationItems)]
         public async Task<PagedResultDto<QuotationItemProductCategoryLookupTableDto>> GetAllProductCategoryForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_productCategoryRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Material.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var productCategoryList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationItemProductCategoryLookupTableDto>();
			foreach(var productCategory in productCategoryList){
				lookupTableDtoList.Add(new QuotationItemProductCategoryLookupTableDto
				{
					Id = productCategory.Id,
					DisplayName = productCategory.Material?.ToString()
				});
			}

            return new PagedResultDto<QuotationItemProductCategoryLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_QuotationItems)]
         public async Task<PagedResultDto<QuotationItemProductSubCategoryLookupTableDto>> GetAllProductSubCategoryForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_productSubCategoryRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.PipeDiameter.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var productSubCategoryList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationItemProductSubCategoryLookupTableDto>();
			foreach(var productSubCategory in productSubCategoryList){
				lookupTableDtoList.Add(new QuotationItemProductSubCategoryLookupTableDto
				{
					Id = productSubCategory.Id,
					DisplayName = productSubCategory.PipeDiameter?.ToString()
				});
			}

            return new PagedResultDto<QuotationItemProductSubCategoryLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_QuotationItems)]
         public async Task<PagedResultDto<QuotationItemUnitPriceLookupTableDto>> GetAllUnitPriceForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_unitPriceRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Price.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var unitPriceList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationItemUnitPriceLookupTableDto>();
			foreach(var unitPrice in unitPriceList){
				lookupTableDtoList.Add(new QuotationItemUnitPriceLookupTableDto
				{
					Id = unitPrice.Id,
					DisplayName = unitPrice.Price?.ToString()
				});
			}

            return new PagedResultDto<QuotationItemUnitPriceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_QuotationItems)]
         public async Task<PagedResultDto<QuotationItemClientUnitPriceLookupTableDto>> GetAllClientUnitPriceForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_clientUnitPriceRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Description.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var clientUnitPriceList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationItemClientUnitPriceLookupTableDto>();
			foreach(var clientUnitPrice in clientUnitPriceList){
				lookupTableDtoList.Add(new QuotationItemClientUnitPriceLookupTableDto
				{
					Id = clientUnitPrice.Id,
					DisplayName = clientUnitPrice.Description?.ToString()
				});
			}

            return new PagedResultDto<QuotationItemClientUnitPriceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_QuotationItems)]
         public async Task<PagedResultDto<QuotationItemQuotationUnitPriceLookupTableDto>> GetAllQuotationUnitPriceForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_quotationUnitPriceRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Description.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var quotationUnitPriceList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<QuotationItemQuotationUnitPriceLookupTableDto>();
			foreach(var quotationUnitPrice in quotationUnitPriceList){
				lookupTableDtoList.Add(new QuotationItemQuotationUnitPriceLookupTableDto
				{
					Id = quotationUnitPrice.Id,
					DisplayName = quotationUnitPrice.Description?.ToString()
				});
			}

            return new PagedResultDto<QuotationItemQuotationUnitPriceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}