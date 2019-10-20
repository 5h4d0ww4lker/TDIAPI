using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class QuotationItemsExcelExporter : EpPlusExcelExporterBase, IQuotationItemsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public QuotationItemsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetQuotationItemForViewDto> quotationItems)
        {
            return CreateExcelPackage(
                "QuotationItems.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("QuotationItems"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Quantity"),
                        L("TotalAmountInETB"),
                        L("Description"),
                        L("CustomUnitPrice"),
                        (L("Quotation")) + L("QuotationNumber"),
                        (L("ProductCategory")) + L("Material"),
                        (L("ProductSubCategory")) + L("PipeDiameter")
                        );

                    AddObjects(
                        sheet, 2, quotationItems,
                        _ => _.QuotationItem.Quantity,
                        _ => _.QuotationItem.TotalAmountInETB,
                        _ => _.QuotationItem.Description,
                        _ => _.QuotationItem.CustomUnitPrice,
                        _ => _.QuotationQuotationNumber,
                        _ => _.ProductCategoryMaterial,
                        _ => _.ProductSubCategoryPipeDiameter
                        );

					

                });
        }
    }
}
