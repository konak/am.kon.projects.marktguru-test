using am.kon.projects.marktguru_test.Controllers.Models;
using am.kon.projects.marktguru_test.Models.Products;
using am.kon.projects.marktguru_test.Models.Products.Adapters;
using am.kon.projects.marktguru_test.product.business_logic;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using am.kon.projects.marktguru_test.product.common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace am.kon.projects.marktguru_test.Controllers;

[Authorize]
[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any)]
public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    private readonly ProductManagementService _productManagementService;

    public ProductsController(
        ILogger<ProductsController> logger,
        ProductManagementService productManagementService
        )
    {
        _logger = logger;
        _productManagementService = productManagementService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        ProductsPageBaseModel<IEnumerable<ProductListItemModel>> pageModel =
            new ProductsPageBaseModel<IEnumerable<ProductListItemModel>>();

        try
        {
            ProductActionResult<List<Product>> result = await _productManagementService.GetAll();

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    pageModel.Data = result.Data.ToListItemModel();
                    break;
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    pageModel.Message = result.Message;
                    break;
                
                default:
                    pageModel.Message = "unhandled case during getting getting all products from storage";
                    break;
            }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("Unhandled Exception", "Unknown error.");
            _logger.LogDebug(exception: e, message: "Unhandled exception in Index.");
        }

        return View(pageModel);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductCreateModel productCreateModel)
    {
        if (!ModelState.IsValid)
        {
            return View(productCreateModel);
        }

        Product product = productCreateModel.ToProduct();

        try
        {
            ProductActionResult<Product> result = await _productManagementService.Create(product);
            
            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return RedirectToAction("Index");
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    ModelState.AddModelError("error-info", result.Message);
                    break;
                
                default:
                    ModelState.AddModelError("unhadled-case", "Unhandled case during product item creation.");
                    break;
            }

        }
        catch (Exception ex)
        {
            ModelState.AddModelError("unhadled-exception", "Unhandled exception during product item creation.");
            _logger.LogDebug(ex, "Unhandled exception.");
        }

        return View();
    }
}