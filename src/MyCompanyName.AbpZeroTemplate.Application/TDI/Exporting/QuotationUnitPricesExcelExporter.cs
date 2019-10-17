using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class QuotationUnitPricesExcelExporter : EpPlusExcelExporterBase, IQuotationUnitPricesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public QuotationUnitPricesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetQuotationUnitPriceForViewDto> quotationUnitPrices)
        {
            return CreateExcelPackage(
                "QuotationUnitPrices.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("QuotationUnitPrices"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Description"),
                        L("Unit"),
                        L("Price"),
                        (L("Quotation")) + L("QuotationNumber")
                        );

                    AddObjects(
                        sheet, 2, quotationUnitPrices,
                        _ => _.QuotationUnitPrice.Description,
                        _ => _.QuotationUnitPrice.Unit,
                        _ => _.QuotationUnitPrice.Price,
                        _ => _.QuotationQuotationNumber
                        );

					

                });
        }
    }
}
