using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class ProductCategoriesExcelExporter : EpPlusExcelExporterBase, IProductCategoriesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ProductCategoriesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetProductCategoryForViewDto> productCategories)
        {
            return CreateExcelPackage(
                "ProductCategories.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductCategories"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Material"),
                        L("UOM"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, productCategories,
                        _ => _.ProductCategory.Material,
                        _ => _.ProductCategory.UOM,
                        _ => _.ProductCategory.Description
                        );

					

                });
        }
    }
}
