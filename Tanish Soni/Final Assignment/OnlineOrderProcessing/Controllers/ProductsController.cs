using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace OnlineOrderProcessing.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async  Task<IActionResult> Index(string? Search)
        {
            var model = await _productService.GetAllProducts(Search) ?? new ProductListViewModel();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Adminproducts(string? Search)
        {
            var model = await _productService.GetAllProducts(Search) ?? new ProductListViewModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CreateProductViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Data !");
                return View(Model);
            }

            var result = await _productService.CreateProduct(Model);

            if(!result){
                ModelState.AddModelError("", "Unable to Create Product !");
                return View(Model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var model = await _productService.GetProductForEditById(Id);
            return  View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit (EditProductViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Data !");
                return View(Model);
            }
            var result = await  _productService.EditProduct(Model);
            if (!result) 
            {
                ModelState.AddModelError("", "Unable to Edit Product !");
                return View(Model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete( Guid Id)
        {
           var result =  await _productService.DeleteProduct(Id);

            if(!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Product deleted Unsuccessfully"
                });
            }
            return Json(new
            {
                success = true,
                message = "Product deleted successfully"
            });
        }

     
    
    }
}
