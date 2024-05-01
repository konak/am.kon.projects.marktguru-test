using am.kon.projects.marktguru_test.Models.Products;
using am.kon.projects.marktguru_test.Models.Products.Adapters;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using Microsoft.AspNetCore.Mvc;

namespace am.kon.projects.marktguru_test.Controllers;

public partial class ProductsController
{
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