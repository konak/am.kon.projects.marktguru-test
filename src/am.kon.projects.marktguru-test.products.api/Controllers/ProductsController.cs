using am.kon.projects.marktguru_test.product.business_logic;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using am.kon.projects.marktguru_test.products.api.Models;
using am.kon.projects.marktguru_test.products.api.Models.Adapters;
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
                    return StatusCode(400, result.Message) ;

                default:
                    return StatusCode(400, "unhandled case during getting all products from storage");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Unhandled exception in 'GetProducts'.");
            return StatusCode(500, "Unhandled exception in 'GetProducts'.");
        }
    }
    
    [AllowAnonymous]
    /// <summary>
    /// Endpoint to get a product by provided Id
    /// </summary>
    /// <param name="id">Id of the product to be retrived from the storage</param>
    /// <returns>An instance of a product</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProducts(Guid id)
    {
        try
        {
            ProductActionResult<Product> result = await _productManagementService.GetItem(id);

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return Ok(result.Data);
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    return StatusCode(400, result.Message) ;

                default:
                    return StatusCode(400, "unhandled case during getting all products from storage");
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Unhandled exception in 'GetProducts' by provided Id.");
            return StatusCode(500, "Unhandled exception in 'GetProducts' by provided Id.");
        }
    }

    /// <summary>
    /// Endpoit to create product
    /// </summary>
    /// <param name="product">Instance of the <see cref="ProductCreateModel"/> to be created in the storage</param>
    /// <returns>Instance of the product created in storage.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateModel product)
    {
        try
        {
            ProductActionResult<Product> result = await _productManagementService.Create(product.ToProduct());

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return Ok(result.Data);
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    return StatusCode(400, result.Message) ;

                default:
                    return StatusCode(400, "unhandled case during crete product int the storage");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Unhandled exception in 'CreateProducts' by provided Id.");
            return StatusCode(500, "Unhandled exception in 'CreateProducts' by provided Id.");
        }
    }

    /// <summary>
    /// Endpoit to update product data
    /// </summary>
    /// <param name="product">Instance of the <see cref="ProductEditModel"/> to be updated in the storage</param>
    /// <returns>Instance of the product updated in storage.</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductEditModel product)
    {
        try
        {
            ProductActionResult<Product> result = await _productManagementService.Update(product.ToProduct());

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return Ok(result.Data);
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    return StatusCode(400, result.Message) ;

                default:
                    return StatusCode(400, "unhandled case during product edit.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Unhandled exception in 'EditProducts' by provided Id.");
            return StatusCode(500, "Unhandled exception in 'EditProducts' by provided Id.");
        }
    }
    
    /// <summary>
    /// Endpoit to delete product from storage
    /// </summary>
    /// <param name="id">Id of the product to delete from the storage.</param>
    /// <returns>Instance of the product deleted from storage.</returns>
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            ProductActionResult<Product> result = await _productManagementService.Delete(id);

            switch (result.ActionResult)
            {
                case ProductActionResultTypes.Ok:
                    return Ok(result.Data);
                
                case ProductActionResultTypes.Error:
                case ProductActionResultTypes.Info:
                    return StatusCode(400, result.Message) ;

                default:
                    return StatusCode(400, "unhandled case during product delete.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Unhandled exception in 'DeleteProduct' by provided Id.");
            return StatusCode(500, "Unhandled exception in 'DeleteProduct' by provided Id.");
        }
    }
}