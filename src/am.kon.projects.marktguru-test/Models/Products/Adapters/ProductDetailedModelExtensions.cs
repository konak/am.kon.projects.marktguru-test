using am.kon.projects.marktguru_test.product.common.Models;

namespace am.kon.projects.marktguru_test.Models.Products.Adapters;

/// <summary>
/// Class providing extension methods for instances of <see cref="ProductDetailedModel"/> class.
/// </summary>
public static class ProductDetailedModelExtensions
{
    /// <summary>
    /// Extension method for instance of <see cref="Product"/>
    /// generating instance of <see cref="ProductDetailedModel"/>
    /// </summary>
    /// <param name="product">Instance of <see cref="Product"/> to be converted.</param>
    /// <returns>New instance of <see cref="ProductDetailedModel"/></returns>
    public static ProductDetailedModel ToProductDetailedModel(this Product product)
    {
        return new ProductDetailedModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Available = product.Available,
            Description = product.Description,
            DateCreated = product.DateCreated,
            Version = product.Version
        };
    }
}