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
                        L("ShipmentTypes"),
                        L("DiscountInPercent"),
                        L("DiscountInAmount"),
                        L("PlaceOfDelivery"),
                        L("Status"),
                        L("CheckedBy"),
                        L("ApprovedBy"),
                        (L("Client")) + L("ClientName"),
                        (L("ProductCategory")) + L("Material"),
                        (L("PaymentTerm")) + L("Description"),
                        (L("PriceValidity")) + L("Description")
                        );

                    AddObjects(
                        sheet, 2, quotations,
                        _ => _.Quotation.QuotationNumber,
                        _ => _.Quotation.ShipmentTypes,
                        _ => _.Quotation.DiscountInPercent,
                        _ => _.Quotation.DiscountInAmount,
                        _ => _.Quotation.PlaceOfDelivery,
                        _ => _.Quotation.Status,
                        _ => _.Quotation.CheckedBy,
                        _ => _.Quotation.ApprovedBy,
                        _ => _.ClientClientName,
                        _ => _.ProductCategoryMaterial,
                        _ => _.PaymentTermDescription,
                        _ => _.PriceValidityDescription
                        );

					

                });
        }
    }
}
