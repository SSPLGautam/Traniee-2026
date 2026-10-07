using Microsoft.EntityFrameworkCore;
using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class ReportRepository :IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public async Task<SalesReportViewModel> GetSalesReport(
    DateTime? fromDate,
    DateTime? toDate,
    Guid? productId,
    OrderStatus? status)
        {
            var orders = _context.Orders
                .AsNoTracking()
                .AsQueryable();

            if (fromDate.HasValue)
            {
                orders = orders.Where(x =>
                    x.CreatedAt >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                orders = orders.Where(x =>
                    x.CreatedAt < toDate.Value.Date.AddDays(1));
            }

            if (productId.HasValue)
            {
                orders = orders.Where(x =>
                    x.OrderItems.Any(i =>
                        i.ProductId == productId.Value));
            }

            if (status.HasValue)
            {
                orders = orders.Where(x =>
                    x.Status == status.Value);
            }


            var totalOrders = await orders.CountAsync();


            var successfulOrders = await orders
                .Where(x =>
                    x.Status == OrderStatus.Paid ||
                    x.Status == OrderStatus.Processing ||
                    x.Status == OrderStatus.Shipped ||
                    x.Status == OrderStatus.Delivered)
                .CountAsync();


            var cancelledOrders = await orders
                .Where(x => x.Status == OrderStatus.Cancelled)
                .CountAsync();

            var totalRevenue = await orders
                .Where(x =>
                    x.Status == OrderStatus.Paid ||
                    x.Status == OrderStatus.Processing ||
                    x.Status == OrderStatus.Shipped ||
                    x.Status == OrderStatus.Delivered)
                .SelectMany(x => x.OrderItems)
                .SumAsync(x => x.Quantity * x.UnitPrice);


            var topProducts = await orders
                .Where(x =>
                    x.Status == OrderStatus.Paid ||
                    x.Status == OrderStatus.Processing ||
                    x.Status == OrderStatus.Shipped ||
                    x.Status == OrderStatus.Delivered)
                .SelectMany(x => x.OrderItems)
                .GroupBy(x => new
                {
                    x.ProductId,
                    x.Product.Name
                })
                .Select(x => new TopProductViewModel
                {
                    ProductName = x.Key.Name,
                    QuantitySold = x.Sum(i => i.Quantity)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToListAsync();


            return new SalesReportViewModel
            {
                TotalOrders = totalOrders,
                SuccessfulOrders = successfulOrders,
                CancelledOrders = cancelledOrders,
                TotalRevenue = totalRevenue,
                TopProducts = topProducts
            };
        }
    }
}
