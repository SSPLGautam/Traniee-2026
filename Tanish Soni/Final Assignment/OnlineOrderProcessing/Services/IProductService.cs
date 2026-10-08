using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IProductService
    {
        Task<Result<ProductListViewModel>> GetAllProducts(
    string? search,
    int page = 1,
    int pageSize = 5);

        Task<Result<bool>> CreateProduct(CreateProductViewModel Model);

        Task<Result<EditProductViewModel>> GetProductForEditById(Guid Id);

        Task<Result<bool>> EditProduct(EditProductViewModel Model);
        Task<Result<bool>> DeleteProduct(Guid Id);

    }
}
