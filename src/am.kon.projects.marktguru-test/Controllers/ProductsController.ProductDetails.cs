using am.kon.projects.marktguru_test.Models.Products.Adapters;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using Microsoft.AspNetCore.Mvc;

namespace am.kon.projects.marktguru_test.Controllers;

public partial class ProductsController
{
    [HttpGet]
    public async Task<IActionResult> ProductDetails(Guid id)
    {
        try
        {
            ProductActionResult<Product> result = await _productManagementService.GetItem(id);

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return View(result.Data.ToProductDetailedModel());
                
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
    
}