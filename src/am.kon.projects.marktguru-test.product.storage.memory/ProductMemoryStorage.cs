using am.kon.projects.marktguru_test.product.abstraction;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using am.kon.projects.marktguru_test.product.common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace am.kon.projects.marktguru_test.product.storage.memory;

/// <summary>
/// In-memory implementation of the storage for products. 
/// </summary>
public class ProductMemoryStorage : ProductServiceBase, IProductStorage
{
    public ProductMemoryStorage(
        ILogger<ProductMemoryStorage> logger,
        IConfiguration configuration
        ) : base(logger, configuration)
    {
    }

    /// <summary>
    /// Get list of all products from the storage.
    /// </summary>
    /// <returns>Returns list of all available products.</returns>
    public Task<ProductActionResult<List<Product>>> GetAll()
    {
        ProductActionResult<List<Product>> result = new ProductActionResult<List<Product>>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };
        
        return Task.FromResult<ProductActionResult<List<Product>>>(result);
    }

    /// <summary>
    /// Get product item by provided Id
    /// </summary>
    /// <param name="id">Id of the product to be read from storage.</param>
    /// <returns>Instance of the product read from the storage</returns>
    public Task<ProductActionResult<Product>> GetItem(Guid id)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };
        
        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    /// <summary>
    /// Create product in the storage.
    /// </summary>
    /// <param name="product">Instance of the product to be created in the storage.</param>
    /// <returns>Instance of the already created product.</returns>
    public Task<ProductActionResult<Product>> Create(Product product)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };
        
        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    /// <summary>
    /// Update product data in the storage.
    /// </summary>
    /// <param name="product">instance of the project with changes to be updated in the storage.</param>
    /// <returns>Instance of the already Updated product.</returns>
    public Task<ProductActionResult<Product>> Update(Product product)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };

        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    /// <summary>
    /// Delete product data from the storage.
    /// </summary>
    /// <param name="productId">Id of the product to be deleted.</param>
    /// <returns>Deleted instance of the product.</returns>
    public Task<ProductActionResult<Product>> Delete(Guid productId)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };

        return Task.FromResult<ProductActionResult<Product>>(result);
    }

}