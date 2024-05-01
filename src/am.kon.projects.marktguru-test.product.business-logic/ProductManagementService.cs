using am.kon.projects.marktguru_test.product.abstraction;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;
using am.kon.projects.marktguru_test.product.common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace am.kon.projects.marktguru_test.product.business_logic;

public class ProductManagementService : ProductServiceBase
{
    private readonly IProductStorage _storage;

    public ProductManagementService(
        ILogger<ProductManagementService> logger,
        IConfiguration configuration,
        IProductStorage storage
        ) : base(logger, configuration)
    {
        _storage = storage;
    }

    /// <summary>
    /// Get list of all products from the storage.
    /// </summary>
    /// <returns>Action result with list of all available products. In case if action will fail it will return an error message in action result.</returns>
    public Task<ProductActionResult<List<Product>>> GetAll()
    {
        // lat's not have any logic here before and after getting all data from the storage.
        return _storage.GetAll();
    }

    /// <summary>
    /// Get product item by provided Id
    /// </summary>
    /// <param name="id">Id of the product to be read from storage.</param>
    /// <returns>Action result with instance of the product read from the storage. In case if action will fail it will return an error message in action result.</returns>
    public Task<ProductActionResult<Product>> GetItem(Guid id)
    {
        // Add some business logic here to be done before reading data from storage.

        return _storage.GetItem(id);
        
        // In this case nothing should be done after getting data from storage
    }

    /// <summary>
    /// Create product in the storage.
    /// </summary>
    /// <param name="product">Instance of the product to be created in the storage. In case if action will fail it will return an error message in action result.</param>
    /// <returns>Action result with instance of the already created product. In case if action will fail it will return an error message in action result.</returns>
    public async Task<ProductActionResult<Product>> Create(Product product)
    {
        // Add some business logic here to be done before writing data to storage.

        ProductActionResult<Product> result = await  _storage.Create(product);
        
        // Add some business logic here to be done after data was written to storage.

        return result;
    }

    /// <summary>
    /// Update product data in the storage.
    /// </summary>
    /// <param name="product">instance of the project with changes to be updated in the storage.</param>
    /// <returns>Action result with instance of the already Updated product. In case if action will fail it will return an error message in action result.</returns>
    public async Task<ProductActionResult<Product>> Update(Product product)
    {
        // Add some business logic here to be done before updating data in storage.

        ProductActionResult<Product> result = await  _storage.Update(product);
        
        // Add some business logic here to be done after data was updated in storage.

        return result;
    }

    /// <summary>
    /// Delete product data from the storage.
    /// </summary>
    /// <param name="productId">Id of the product to be deleted.</param>
    /// <returns>Action result with deleted instance of the product. In case if action will fail it will return an error message in action result.</returns>
    public Task<ProductActionResult<Product>> Delete(Guid productId)
    {
        return _storage.Delete(productId);
    }
}