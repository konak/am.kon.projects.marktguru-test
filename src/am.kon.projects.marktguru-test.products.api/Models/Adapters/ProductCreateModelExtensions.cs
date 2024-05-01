using am.kon.projects.marktguru_test.product.common.Models;

namespace am.kon.projects.marktguru_test.products.api.Models.Adapters;

/// <summary>
/// Class with extension functions for <see cref="ProductCreateModel"/>
/// </summary>
public static class ProductCreateModelExtensions
{
    /// <summary>
    /// Extension method for instance of <see cref="ProductCreateModel"/>
    /// to generate a new instance of <see cref="Product"/>.
    /// </summary>
    /// <param name="productCreateModel">Instance of <see cref="ProductCreateModel"/>
    /// to be used to generate new instance of <see cref="Product"/>.</param>
    /// <returns>Returns new instance of <see cref="Product"/> </returns>
    public static Product ToProduct(this ProductCreateModel productCreateModel)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = productCreateModel.Name,
            Price = productCreateModel.Price,
            Available = productCreateModel.Available,
            Description = productCreateModel.Description,
            DateCreated = DateTime.UtcNow
        };
    }
    
}