using System.Collections.Concurrent;
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
    private readonly ConcurrentDictionary<Guid, Product> _storageById;
    private readonly ConcurrentDictionary<string, Product> _storageByName;
    
    public ProductMemoryStorage(
        ILogger<ProductMemoryStorage> logger,
        IConfiguration configuration
        ) : base(logger, configuration)
    {
        _storageById = new ConcurrentDictionary<Guid, Product>();
        _storageByName = new ConcurrentDictionary<string, Product>();
    }

    /// <summary>
    /// Get list of all products from the storage.
    /// </summary>
    /// <returns>Returns list of all available products.</returns>
    public Task<ProductActionResult<List<Product>>> GetAll()
    {
        ProductActionResult<List<Product>> result = new ProductActionResult<List<Product>>
        {
            ActionResult = ProductActionResultTypes.Ok,
            Data = new List<Product>(_storageById.Values)
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
        ProductActionResult<Product> result = new ProductActionResult<Product>();

        if (_storageById.TryGetValue(id, out Product product))
        {
            result.ActionResult = ProductActionResultTypes.Ok;
            result.Data = product;
        }
        else
        {
            result.ActionResult = ProductActionResultTypes.Error;
            result.Message = $"Product with id: {id} was not found.";
        }
        
        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    /// <summary>
    /// Create product in the storage.
    /// </summary>
    /// <param name="product">Instance of the product to be created in the storage.</param>
    /// <returns>Instance of the already created product.</returns>
    public Task<ProductActionResult<Product>> Create(Product product)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>();

        lock (product)
        {
            if (!_storageByName.TryAdd(product.Name, product))
            {
                result.ActionResult = ProductActionResultTypes.Error;
                result.Message = "Product with the same name exists.";

                return Task.FromResult(result);
            }

            if (!_storageById.TryAdd(product.Id, product))
            {
                _storageByName.TryRemove(product.Name, out _);

                result.ActionResult = ProductActionResultTypes.Error;
                result.Message = "Product with the same Id exists.";

                return Task.FromResult(result);
            }
        }
        
        result.ActionResult = ProductActionResultTypes.Ok;
        result.Data = product;
        
        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    /// <summary>
    /// Update product data in the storage.
    /// </summary>
    /// <param name="product">instance of the project with changes to be updated in the storage.</param>
    /// <returns>Instance of the already Updated product.</returns>
    public Task<ProductActionResult<Product>> Update(Product product)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>();

        if (_storageById.TryGetValue(product.Id, out Product currentProduct))
        {
            lock (currentProduct)
            {
                if (currentProduct.Version != product.Version)
                {
                    result.ActionResult = ProductActionResultTypes.Info;
                    result.Message = "Product was modified in other session.";

                    return Task.FromResult(result);
                }

                if (currentProduct.Name != product.Name)
                {
                    if (!_storageByName.TryAdd(product.Name, product))
                    {
                        result.ActionResult = ProductActionResultTypes.Error;
                        result.Message = "Product with the same name already exists.";

                        return Task.FromResult(result);
                    }

                    _storageByName.TryRemove(currentProduct.Name, out _);
                    currentProduct.Name = product.Name;
                }

                currentProduct.Available = product.Available;
                currentProduct.Description = product.Description;
                currentProduct.Price = product.Price;
                currentProduct.Version++;
            }

            result.ActionResult = ProductActionResultTypes.Ok;
            result.Data = currentProduct;
        }
        else
        {
            result.ActionResult = ProductActionResultTypes.Error;
            result.Message = $"Product with id: {product.Id} was not found";
        }
        
        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    /// <summary>
    /// Delete product data from the storage.
    /// </summary>
    /// <param name="productId">Id of the product to be deleted.</param>
    /// <returns>Deleted instance of the product.</returns>
    public Task<ProductActionResult<Product>> Delete(Guid productId)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>();

        if (_storageById.TryRemove(productId, out Product product))
        {
            lock (product)
            {
                _storageByName.TryRemove(product.Name, out _);
            }

            result.ActionResult = ProductActionResultTypes.Ok;
            result.Data = product;
        }
        else
        {
            result.ActionResult = ProductActionResultTypes.Error;
            result.Message = $"Product with id: {productId} was not found";
        }

        return Task.FromResult<ProductActionResult<Product>>(result);
    }
}