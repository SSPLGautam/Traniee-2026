using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IReportService
    {
        Task<SalesReportViewModel> GetSalesReport(
       DateTime? fromDate,
       DateTime? toDate,
       Guid? productId,
       OrderStatus? status);
    }
}
