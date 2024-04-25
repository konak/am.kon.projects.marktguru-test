using am.kon.projects.marktguru_test.product.abstraction;
using am.kon.projects.marktguru_test.product.common.Action;
using am.kon.projects.marktguru_test.product.common.Models;

namespace am.kon.projects.marktguru_test.product.storage.mssql;

public class ProductMsSqlStorage : IProductStorage
{
    public Task<ProductActionResult<List<Product>>> GetAll()
    {
        ProductActionResult<List<Product>> result = new ProductActionResult<List<Product>>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };
        
        return Task.FromResult<ProductActionResult<List<Product>>>(result);
    }

    public Task<ProductActionResult<Product>> GetItem(Guid id)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };
        
        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    public Task<ProductActionResult<Product>> Create(Product product)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };
        
        return Task.FromResult<ProductActionResult<Product>>(result);
    }

    public Task<ProductActionResult<Product>> Update(Product product)
    {
        ProductActionResult<Product> result = new ProductActionResult<Product>
        {
            ActionResult = ProductActionResultTypes.Error,
            Message = "Not Implemented."
        };

        return Task.FromResult<ProductActionResult<Product>>(result);
    }

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