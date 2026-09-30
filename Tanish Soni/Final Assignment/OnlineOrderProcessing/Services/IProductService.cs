using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IProductService
    {
        Task<ProductListViewModel> GetAllProducts(string? Search);

        Task<bool> CreateProduct(CreateProductViewModel Model);

        Task<EditProductViewModel> GetProductForEditById(Guid Id);

        Task<bool> EditProduct(EditProductViewModel Model);
        Task<bool> DeleteProduct(Guid Id);

    }
}
