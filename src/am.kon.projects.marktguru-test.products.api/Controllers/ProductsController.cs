using am.kon.projects.marktguru_test.product.business_logic;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace am.kon.projects.marktguru_test.products.api.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller providing CRUD functionality for products.
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/products")]
public class ProductsController : ControllerBase
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
    
    /// <summary>
    /// Endpoint to get all available products.
    /// </summary>
    /// <returns>An array of all available products in the storage.</returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            ProductActionResult<List<Product>> result = await _productManagementService.GetAll();

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return Ok(result.Data);
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    return StatusCode(500, result.Message) ;

                default:
                    return StatusCode(500, "unhandled case during getting all products from storage");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(exception: e, message: "Unhandled exception in 'GetProducts'.");
            return StatusCode(500, "Unhandled exception in 'GetProducts'.");
        }
    }
    
    /// <summary>
    /// Endpoint to get an endpoint by provided Id
    /// </summary>
    /// <param name="id">Id of the product to be retrived from the storage</param>
    /// <returns>An instance of a product</returns>
    [HttpGet("{id}")]
    public Task<IActionResult> GetProducts(Guid id)
    {
        return Task.FromResult<IActionResult>(Ok(new {data = "data for product with id: " + id + " should be here"}));
    }
}