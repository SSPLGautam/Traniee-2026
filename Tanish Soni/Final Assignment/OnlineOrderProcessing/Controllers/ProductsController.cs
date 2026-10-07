using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
        public async  Task<IActionResult> Index(string? Search,int page=1)
        {
            var model = await _productService.GetAllProducts(
                Search,
                page,
                5);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Adminproducts(
    string? Search,
    int page = 1)
        {
            var model = await _productService.GetAllProducts(
                Search,
                page,
                5);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CreateProductViewModel());
        }
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var model = await _productService.GetProductForEditById(Id);
            return  View(model);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
