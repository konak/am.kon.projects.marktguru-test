using am.kon.projects.marktguru_test.product.common.Models;

namespace am.kon.projects.marktguru_test.Models.Products.Adapters;

/// <summary>
/// Class for extension methods of <see cref="ProductCreateModel"/> instances.
/// </summary>
public static class ProductCreateModelExtensions
{
    /// <summary>
    /// Extension method converting instance of <see cref="ProductCreateModel"/> into <see cref="Product"/>
    /// </summary>
    /// <param name="productCreateModel">Instance of <see cref="ProductCreateModel"/> class to be converted.</param>
    /// <returns>New instance of <see cref="Product"/>.</returns>
    public static Product ToProduct(this ProductCreateModel productCreateModel)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = productCreateModel.Name,
            Price = productCreateModel.Price,
            Available = productCreateModel.Available,
            Description = productCreateModel.Description,
            DateCreated = DateTime.UtcNow,
            Version = 0
        };
    }
}