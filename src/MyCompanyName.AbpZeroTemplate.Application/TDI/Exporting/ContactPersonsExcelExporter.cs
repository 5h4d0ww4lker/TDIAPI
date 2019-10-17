using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class ContactPersonsExcelExporter : EpPlusExcelExporterBase, IContactPersonsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ContactPersonsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetContactPersonForViewDto> contactPersons)
        {
            return CreateExcelPackage(
                "ContactPersons.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ContactPersons"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("FullName"),
                        L("PhoneNumber")
                        );

                    AddObjects(
                        sheet, 2, contactPersons,
                        _ => _.ContactPerson.FullName,
                        _ => _.ContactPerson.PhoneNumber
                        );

					

                });
        }
    }
}
