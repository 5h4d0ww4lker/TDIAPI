using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class UnitPricesExcelExporter : EpPlusExcelExporterBase, IUnitPricesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UnitPricesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUnitPriceForViewDto> unitPrices)
        {
            return CreateExcelPackage(
                "UnitPrices.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("UnitPrices"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Price"),
                        L("Unit"),
                        (L("Product")) + L("Description")
                        );

                    AddObjects(
                        sheet, 2, unitPrices,
                        _ => _.UnitPrice.Price,
                        _ => _.UnitPrice.Unit,
                        _ => _.ProductDescription
                        );

					

                });
        }
    }
}
