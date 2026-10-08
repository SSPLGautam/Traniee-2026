using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Result< SalesReportViewModel>> GetSalesReport(
            DateTime? fromDate,
            DateTime? toDate,
            Guid? productId,
            OrderStatus? status)
        {
            return Result<SalesReportViewModel>.Success( await _reportRepository.GetSalesReport(
                fromDate,
                toDate,
                productId,
                status));
        }
    }
}
