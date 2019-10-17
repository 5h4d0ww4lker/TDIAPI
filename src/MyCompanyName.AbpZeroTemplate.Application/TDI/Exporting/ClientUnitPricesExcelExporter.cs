using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class ClientUnitPricesExcelExporter : EpPlusExcelExporterBase, IClientUnitPricesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClientUnitPricesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClientUnitPriceForViewDto> clientUnitPrices)
        {
            return CreateExcelPackage(
                "ClientUnitPrices.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ClientUnitPrices"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Description"),
                        L("Unit"),
                        L("Price"),
                        (L("Client")) + L("ClientName"),
                        (L("ProductSubCategory")) + L("PipeDiameter")
                        );

                    AddObjects(
                        sheet, 2, clientUnitPrices,
                        _ => _.ClientUnitPrice.Description,
                        _ => _.ClientUnitPrice.Unit,
                        _ => _.ClientUnitPrice.Price,
                        _ => _.ClientClientName,
                        _ => _.ProductSubCategoryPipeDiameter
                        );

					

                });
        }
    }
}
