using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class OrderedProductsExcelExporter : EpPlusExcelExporterBase, IOrderedProductsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public OrderedProductsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetOrderedProductForViewDto> orderedProducts)
        {
            return CreateExcelPackage(
                "OrderedProducts.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("OrderedProducts"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Quantity"),
                        L("TotalAmountInETB"),
                        (L("QuotationItem")) + L("Description")
                        );

                    AddObjects(
                        sheet, 2, orderedProducts,
                        _ => _.OrderedProduct.Quantity,
                        _ => _.OrderedProduct.TotalAmountInETB,
                        _ => _.QuotationItemDescription
                        );

					

                });
        }
    }
}
