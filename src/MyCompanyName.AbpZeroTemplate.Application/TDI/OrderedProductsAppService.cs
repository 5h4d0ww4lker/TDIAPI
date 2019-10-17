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
	[AbpAuthorize(AppPermissions.Pages_OrderedProducts)]
    public class OrderedProductsAppService : AbpZeroTemplateAppServiceBase, IOrderedProductsAppService
    {
		 private readonly IRepository<OrderedProduct> _orderedProductRepository;
		 private readonly IOrderedProductsExcelExporter _orderedProductsExcelExporter;
		 private readonly IRepository<QuotationItem,int> _lookup_quotationItemRepository;
		 

		  public OrderedProductsAppService(IRepository<OrderedProduct> orderedProductRepository, IOrderedProductsExcelExporter orderedProductsExcelExporter , IRepository<QuotationItem, int> lookup_quotationItemRepository) 
		  {
			_orderedProductRepository = orderedProductRepository;
			_orderedProductsExcelExporter = orderedProductsExcelExporter;
			_lookup_quotationItemRepository = lookup_quotationItemRepository;
		
		  }

		 public async Task<PagedResultDto<GetOrderedProductForViewDto>> GetAll(GetAllOrderedProductsInput input)
         {
			
			var filteredOrderedProducts = _orderedProductRepository.GetAll()
						.Include( e => e.QuotationItemFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Quantity.Contains(input.Filter) || e.TotalAmountInETB.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuantityFilter),  e => e.Quantity.ToLower() == input.QuantityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TotalAmountInETBFilter),  e => e.TotalAmountInETB.ToLower() == input.TotalAmountInETBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationItemDescriptionFilter), e => e.QuotationItemFk != null && e.QuotationItemFk.Description.ToLower() == input.QuotationItemDescriptionFilter.ToLower().Trim());

			var pagedAndFilteredOrderedProducts = filteredOrderedProducts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var orderedProducts = from o in pagedAndFilteredOrderedProducts
                         join o1 in _lookup_quotationItemRepository.GetAll() on o.QuotationItemId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetOrderedProductForViewDto() {
							OrderedProduct = new OrderedProductDto
							{
                                Quantity = o.Quantity,
                                TotalAmountInETB = o.TotalAmountInETB,
                                Id = o.Id
							},
                         	QuotationItemDescription = s1 == null ? "" : s1.Description.ToString()
						};

            var totalCount = await filteredOrderedProducts.CountAsync();

            return new PagedResultDto<GetOrderedProductForViewDto>(
                totalCount,
                await orderedProducts.ToListAsync()
            );
         }
		 
		 public async Task<GetOrderedProductForViewDto> GetOrderedProductForView(int id)
         {
            var orderedProduct = await _orderedProductRepository.GetAsync(id);

            var output = new GetOrderedProductForViewDto { OrderedProduct = ObjectMapper.Map<OrderedProductDto>(orderedProduct) };

		    if (output.OrderedProduct.QuotationItemId != null)
            {
                var _lookupQuotationItem = await _lookup_quotationItemRepository.FirstOrDefaultAsync((int)output.OrderedProduct.QuotationItemId);
                output.QuotationItemDescription = _lookupQuotationItem.Description.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_OrderedProducts_Edit)]
		 public async Task<GetOrderedProductForEditOutput> GetOrderedProductForEdit(EntityDto input)
         {
            var orderedProduct = await _orderedProductRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetOrderedProductForEditOutput {OrderedProduct = ObjectMapper.Map<CreateOrEditOrderedProductDto>(orderedProduct)};

		    if (output.OrderedProduct.QuotationItemId != null)
            {
                var _lookupQuotationItem = await _lookup_quotationItemRepository.FirstOrDefaultAsync((int)output.OrderedProduct.QuotationItemId);
                output.QuotationItemDescription = _lookupQuotationItem.Description.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditOrderedProductDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_OrderedProducts_Create)]
		 private async Task Create(CreateOrEditOrderedProductDto input)
         {
            var orderedProduct = ObjectMapper.Map<OrderedProduct>(input);

			

            await _orderedProductRepository.InsertAsync(orderedProduct);
         }

		 [AbpAuthorize(AppPermissions.Pages_OrderedProducts_Edit)]
		 private async Task Update(CreateOrEditOrderedProductDto input)
         {
            var orderedProduct = await _orderedProductRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, orderedProduct);
         }

		 [AbpAuthorize(AppPermissions.Pages_OrderedProducts_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _orderedProductRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetOrderedProductsToExcel(GetAllOrderedProductsForExcelInput input)
         {
			
			var filteredOrderedProducts = _orderedProductRepository.GetAll()
						.Include( e => e.QuotationItemFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Quantity.Contains(input.Filter) || e.TotalAmountInETB.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuantityFilter),  e => e.Quantity.ToLower() == input.QuantityFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TotalAmountInETBFilter),  e => e.TotalAmountInETB.ToLower() == input.TotalAmountInETBFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.QuotationItemDescriptionFilter), e => e.QuotationItemFk != null && e.QuotationItemFk.Description.ToLower() == input.QuotationItemDescriptionFilter.ToLower().Trim());

			var query = (from o in filteredOrderedProducts
                         join o1 in _lookup_quotationItemRepository.GetAll() on o.QuotationItemId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetOrderedProductForViewDto() { 
							OrderedProduct = new OrderedProductDto
							{
                                Quantity = o.Quantity,
                                TotalAmountInETB = o.TotalAmountInETB,
                                Id = o.Id
							},
                         	QuotationItemDescription = s1 == null ? "" : s1.Description.ToString()
						 });


            var orderedProductListDtos = await query.ToListAsync();

            return _orderedProductsExcelExporter.ExportToFile(orderedProductListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_OrderedProducts)]
         public async Task<PagedResultDto<OrderedProductQuotationItemLookupTableDto>> GetAllQuotationItemForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_quotationItemRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Description.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var quotationItemList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<OrderedProductQuotationItemLookupTableDto>();
			foreach(var quotationItem in quotationItemList){
				lookupTableDtoList.Add(new OrderedProductQuotationItemLookupTableDto
				{
					Id = quotationItem.Id,
					DisplayName = quotationItem.Description?.ToString()
				});
			}

            return new PagedResultDto<OrderedProductQuotationItemLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}