using OnlineOrderProcessing.Common;
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
        public async Task<Result<ProductListViewModel>> GetAllProducts(
           string? search,
           int page = 1,
           int pageSize = 5)
        {
            if (page < 1)
            {
                page = 1;
            }

            var totalProducts =
                await _unitOfWork.Products.GetProductCountAsync(search);

            var totalPages =
                (int)Math.Ceiling(totalProducts / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            var products =
                await _unitOfWork.Products.GetAllProductsAsync(
                    search,
                    page,
                    pageSize);

            return Result<ProductListViewModel>.Success( new ProductListViewModel
            {
                Search = search,
                Products = products,

                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            });
        }


        public async Task<Result<bool>> CreateProduct(CreateProductViewModel Model)
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
            return Result<bool>.Success(true);
        }

        public async Task<Result<EditProductViewModel>> GetProductForEditById(Guid Id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(Id);
            return Result<EditProductViewModel>.Success(new EditProductViewModel
            {

                ProductId = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
            });
        }


        public async Task<Result<bool>> EditProduct(EditProductViewModel Model)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(Model.ProductId);
            if (product == null)
            {
                return Result<bool>.Failure("Product not Found");
            }
            product.Name = Model.Name;
            product.Price = Model.Price;
            product.Stock = Model.Stock;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
           
           return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteProduct(Guid Id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(Id);
            if (product==null)
            {
                return Result<bool>.Failure("Product not found");
            }
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        }
}
