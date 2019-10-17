using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class QuotationsExcelExporter : EpPlusExcelExporterBase, IQuotationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public QuotationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetQuotationForViewDto> quotations)
        {
            return CreateExcelPackage(
                "Quotations.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Quotations"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("QuotationNumber"),
                        L("PriceValidity"),
                        L("TermOfPayment"),
                        L("ShipmentTypes"),
                        L("DiscountInPercent"),
                        L("DiscountInAmount"),
                        (L("Client")) + L("ClientName")
                        );

                    AddObjects(
                        sheet, 2, quotations,
                        _ => _.Quotation.QuotationNumber,
                        _ => _.Quotation.PriceValidity,
                        _ => _.Quotation.TermOfPayment,
                        _ => _.Quotation.ShipmentTypes,
                        _ => _.Quotation.DiscountInPercent,
                        _ => _.Quotation.DiscountInAmount,
                        _ => _.ClientClientName
                        );

					

                });
        }
    }
}
