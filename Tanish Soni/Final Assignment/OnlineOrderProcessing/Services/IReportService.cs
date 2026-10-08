using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IReportService
    {
        Task<Result< SalesReportViewModel>> GetSalesReport(
       DateTime? fromDate,
       DateTime? toDate,
       Guid? productId,
       OrderStatus? status);
    }
}
