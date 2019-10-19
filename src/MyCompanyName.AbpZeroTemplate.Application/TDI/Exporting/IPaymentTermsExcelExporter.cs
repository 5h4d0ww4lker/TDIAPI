using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.TDI.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TDI.Exporting
{
    public interface IPaymentTermsExcelExporter
    {
        FileDto ExportToFile(List<GetPaymentTermForViewDto> paymentTerms);
    }
}