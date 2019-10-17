using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class ClientsExcelExporter : EpPlusExcelExporterBase, IClientsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClientsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClientForViewDto> clients)
        {
            return CreateExcelPackage(
                "Clients.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Clients"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ClientName"),
                        L("ClientTin"),
                        L("ClientAddress"),
                        L("ClientPhone"),
                        (L("ContactPerson")) + L("FullName")
                        );

                    AddObjects(
                        sheet, 2, clients,
                        _ => _.Client.ClientName,
                        _ => _.Client.ClientTin,
                        _ => _.Client.ClientAddress,
                        _ => _.Client.ClientPhone,
                        _ => _.ContactPersonFullName
                        );

					

                });
        }
    }
}
