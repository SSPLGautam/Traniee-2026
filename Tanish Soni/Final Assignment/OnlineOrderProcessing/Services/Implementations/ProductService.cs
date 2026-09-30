using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork) {

            _unitOfWork = unitOfWork;
        }


        public async Task<ProductListViewModel> GetAllProducts(string? Search)
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync(Search);
            return new ProductListViewModel
            {
                Search = Search,
                Products = products
            };
        }

       

        public async Task<bool> CreateProduct(CreateProductViewModel Model)
        {
            var sku = "SKU-" + Guid.NewGuid().ToString("N")[..8].ToUpper();
            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = Model.Name,
                SKU = sku,
                Price = Model.Price,
                Stock = Model.Stock,
               
            };

            await _unitOfWork.Products.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<EditProductViewModel> GetProductForEditById(Guid Id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(Id);
            return new EditProductViewModel
            {

                ProductId = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
            };
        }


        public async Task<bool> EditProduct(EditProductViewModel Model)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(Model.ProductId);
            if (product == null)
            {
                return false;
            }
            product.Name = Model.Name;
            product.Price = Model.Price;
            product.Stock = Model.Stock;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
           
           return true;
        }

        public async Task<bool> DeleteProduct(Guid Id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(Id);
            if (product==null)
            {
                return false;
            }
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        }
}
