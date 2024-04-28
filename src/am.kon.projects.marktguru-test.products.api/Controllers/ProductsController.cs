using Microsoft.AspNetCore.Authorization;

namespace am.kon.projects.marktguru_test.products.api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public Task<IActionResult> GetProducts()
    {
        return Task.FromResult<IActionResult>(CreatedAtAction(nameof(GetProducts), "{data:'all products list should be here.'}"));
    }
    
    [HttpGet("{id}")]
    public Task<IActionResult> GetProducts(Guid id)
    {
        return Task.FromResult<IActionResult>(CreatedAtAction(nameof(GetProducts),
            "{data:'data for product with id: " + id + " should be here'}"));
    }
}