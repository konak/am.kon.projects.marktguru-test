using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace am.kon.projects.marktguru_test.products.api.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller providing CRUD functionality for products.
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/products")]
[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client)]
public class ProductsController : ControllerBase
{
    /// <summary>
    /// Endpoint to get all available products.
    /// </summary>
    /// <returns>An array of all available products in the storage.</returns>
    [AllowAnonymous]
    [HttpGet]
    public Task<IActionResult> GetProducts()
    {
        return Task.FromResult<IActionResult>(Ok(new { data = "list of all products will be returned here." }));
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