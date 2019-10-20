using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public class ProductSubCategoriesExcelExporter : EpPlusExcelExporterBase, IProductSubCategoriesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ProductSubCategoriesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetProductSubCategoryForViewDto> productSubCategories)
        {
            return CreateExcelPackage(
                "ProductSubCategories.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductSubCategories"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("PipeDiameter"),
                        L("WallS"),
                        L("SDR"),
                        L("PN"),
                        L("WPM"),
                        L("OutputKGH"),
                        L("HauiOffSpeed"),
                        L("OutputTA"),
                        L("ProductionTimeKMA"),
                        L("ProductionPipeLengthKMA"),
                        L("PipeLengthM"),
                        L("Extruder"),
                        L("PipeHead"),
                        L("UnitPrice"),
                        (L("ProductCategory")) + L("Material")
                        );

                    AddObjects(
                        sheet, 2, productSubCategories,
                        _ => _.ProductSubCategory.PipeDiameter,
                        _ => _.ProductSubCategory.WallS,
                        _ => _.ProductSubCategory.SDR,
                        _ => _.ProductSubCategory.PN,
                        _ => _.ProductSubCategory.WPM,
                        _ => _.ProductSubCategory.OutputKGH,
                        _ => _.ProductSubCategory.HauiOffSpeed,
                        _ => _.ProductSubCategory.OutputTA,
                        _ => _.ProductSubCategory.ProductionTimeKMA,
                        _ => _.ProductSubCategory.ProductionPipeLengthKMA,
                        _ => _.ProductSubCategory.PipeLengthM,
                        _ => _.ProductSubCategory.Extruder,
                        _ => _.ProductSubCategory.PipeHead,
                        _ => _.ProductSubCategory.UnitPrice,
                        _ => _.ProductCategoryMaterial
                        );

					

                });
        }
    }
}
