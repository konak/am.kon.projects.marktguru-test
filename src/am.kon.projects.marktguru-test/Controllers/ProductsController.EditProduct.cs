using am.kon.projects.marktguru_test.Models.Products;
using am.kon.projects.marktguru_test.Models.Products.Adapters;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using Microsoft.AspNetCore.Mvc;

namespace am.kon.projects.marktguru_test.Controllers;

public partial class ProductsController
{
    [HttpGet]
    public async Task<IActionResult> EditProduct(Guid id)
    {
        try
        {
            ProductActionResult<Product> result = await _productManagementService.GetItem(id);

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return View(result.Data.ToProductEditModel());
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    ModelState.AddModelError("error-info", result.Message);
                    break;
                
                case ProductActionResultTypes.Unknown:
                    ModelState.AddModelError("unhandled-case", "Unhandled case on getting product details.");
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhadled exception in 'ProductDetails'.");
            ModelState.AddModelError("unhandled-exception", "Unhadled exception.");
        }
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductEditModel productEditModel)
    {
        if (!ModelState.IsValid)
        {
            return View(productEditModel);
        }

        Product product = productEditModel.ToProduct();

        try
        {
            ProductActionResult<Product> result = await _productManagementService.Update(product);
            
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