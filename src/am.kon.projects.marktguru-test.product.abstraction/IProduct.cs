using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;

namespace am.kon.projects.marktguru_test.product.abstraction;

/// <summary>
/// Interface providing abstraction for methods of product storage.
/// </summary>
public interface IProductStorage
{
    /// <summary>
    /// Get list of all products from the storage.
    /// </summary>
    /// <returns>Returns list of all available products.</returns>
    Task<ProductActionResult<List<Product>>> GetAll();
    
    /// <summary>
    /// Get product item by provided Id
    /// </summary>
    /// <param name="id">Id of the product to be read from storage.</param>
    /// <returns>Instance of the product read from the storage</returns>
    Task<ProductActionResult<Product>> GetItem(Guid id);
    
    /// <summary>
    /// Create product in the storage.
    /// </summary>
    /// <param name="product">Instance of the product to be created in the storage.</param>
    /// <returns>Instance of the already created product.</returns>
    Task<ProductActionResult<Product>> Create(Product product);
    
    /// <summary>
    /// Update product data in the storage.
    /// </summary>
    /// <param name="product">instance of the project with changes to be updated in the storage.</param>
    /// <returns>Instance of the already Updated product.</returns>
    Task<ProductActionResult<Product>> Update(Product product);
    
    /// <summary>
    /// Delete product data from the storage.
    /// </summary>
    /// <param name="productId">Id of the product to be deleted.</param>
    /// <returns>Deleted instance of the product.</returns>
    Task<ProductActionResult<Product>> Delete(Guid productId);
}