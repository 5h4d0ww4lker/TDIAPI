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
		 

		  public QuotationItemsAppService(IRepository<QuotationItem> quotationItemRepository, IQuotationItemsExcelExporter quotationItemsExcelExporter , IRepository<Quotation, int> lookup_quotationRepository, IRepository<ProductCategory, int> lookup_productCategoryRepository, IRepository<ProductSubCategory, int> lookup_productSubCategoryRepository) 
		  {
			_quotationItemRepository = quotationItemRepository;
			_quotationItemsExcelExporter = quotationItemsExcelExporter;
			_lookup_quotationRepository = lookup_quotationRepository;
		_lookup_productCategoryRepository = lookup_productCategoryRepository;
		_lookup_productSubCategoryRepository = lookup_productSubCategoryRepository;
		
		  }

		 public async Task<PagedResultDto<GetQuotationItemForViewDto>> GetAll(GetAllQuotationItemsInput input)
         {
			
			var filteredQuotationItems = _quotationItemRepository.GetAll()
						.Include( e => e.QuotationFk)
						.Include( e => e.ProductCategoryFk)
						.Include( e => e.ProductSubCategoryFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Quantity.Contains(input.Filter) || e.TotalAmountInETB.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.CustomUnitPrice.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuantityFilter),  e => e.Quantity.ToLower() == input.QuantityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TotalAmountInETBFilter),  e => e.TotalAmountInETB.ToLower() == input.TotalAmountInETBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CustomUnitPriceFilter),  e => e.CustomUnitPrice.ToLower() == input.CustomUnitPriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationQuotationNumberFilter), e => e.QuotationFk != null && e.QuotationFk.QuotationNumber.ToLower() == input.QuotationQuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductSubCategoryPipeDiameterFilter), e => e.ProductSubCategoryFk != null && e.ProductSubCategoryFk.PipeDiameter.ToLower() == input.ProductSubCategoryPipeDiameterFilter.ToLower().Trim());

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
                         
                         select new GetQuotationItemForViewDto() {
							QuotationItem = new QuotationItemDto
							{
                                Quantity = o.Quantity,
                                TotalAmountInETB = o.TotalAmountInETB,
                                Description = o.Description,
                                CustomUnitPrice = o.CustomUnitPrice,
                                Id = o.Id
							},
                         	QuotationQuotationNumber = s1 == null ? "" : s1.QuotationNumber.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString(),
                         	ProductSubCategoryPipeDiameter = s3 == null ? "" : s3.PipeDiameter.ToString()
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
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Quantity.Contains(input.Filter) || e.TotalAmountInETB.Contains(input.Filter) || e.Description.Contains(input.Filter) || e.CustomUnitPrice.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuantityFilter),  e => e.Quantity.ToLower() == input.QuantityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TotalAmountInETBFilter),  e => e.TotalAmountInETB.ToLower() == input.TotalAmountInETBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CustomUnitPriceFilter),  e => e.CustomUnitPrice.ToLower() == input.CustomUnitPriceFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationQuotationNumberFilter), e => e.QuotationFk != null && e.QuotationFk.QuotationNumber.ToLower() == input.QuotationQuotationNumberFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductCategoryMaterialFilter), e => e.ProductCategoryFk != null && e.ProductCategoryFk.Material.ToLower() == input.ProductCategoryMaterialFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductSubCategoryPipeDiameterFilter), e => e.ProductSubCategoryFk != null && e.ProductSubCategoryFk.PipeDiameter.ToLower() == input.ProductSubCategoryPipeDiameterFilter.ToLower().Trim());

			var query = (from o in filteredQuotationItems
                         join o1 in _lookup_quotationRepository.GetAll() on o.QuotationId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_productCategoryRepository.GetAll() on o.ProductCategoryId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_productSubCategoryRepository.GetAll() on o.ProductSubCategoryId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         select new GetQuotationItemForViewDto() { 
							QuotationItem = new QuotationItemDto
							{
                                Quantity = o.Quantity,
                                TotalAmountInETB = o.TotalAmountInETB,
                                Description = o.Description,
                                CustomUnitPrice = o.CustomUnitPrice,
                                Id = o.Id
							},
                         	QuotationQuotationNumber = s1 == null ? "" : s1.QuotationNumber.ToString(),
                         	ProductCategoryMaterial = s2 == null ? "" : s2.Material.ToString(),
                         	ProductSubCategoryPipeDiameter = s3 == null ? "" : s3.PipeDiameter.ToString()
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
					DisplayName = productSubCategory.PipeDiameter?.ToString(),
                    WallS = productSubCategory.WallS?.ToString(),
                    PN = productSubCategory.PN?.ToString(),
                    UnitPrice = productSubCategory.UnitPrice?.ToString(),
                    WPM = productSubCategory.WPM?.ToString(),
                    Extruder = productSubCategory.Extruder?.ToString(),
                    PipeHead = productSubCategory.PipeHead?.ToString()

                });
			}

            return new PagedResultDto<QuotationItemProductSubCategoryLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}