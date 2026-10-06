using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Repositories
{
    public interface IReportRepository
    {
        Task<SalesReportViewModel> GetSalesReport(
    DateTime? fromDate,
    DateTime? toDate,
    Guid? productId,
    OrderStatus? status);
    }
}
