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
//[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any)]
public partial class ProductsController : Controller
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
}