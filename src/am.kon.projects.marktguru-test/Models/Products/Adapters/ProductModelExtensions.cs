using am.kon.projects.marktguru_test.product.common.Models;

namespace am.kon.projects.marktguru_test.Models.Products.Adapters;

/// <summary>
/// Static class containing extension methods for instance of <see cref="Product"/> class.
/// </summary>
internal static class ProductModelExtensions
{
    /// <summary>
    /// Extension method transform instance of <see cref="Product"/> to
    /// <see cref="ProductDetailedModel"/> copying all necessary properies.
    /// </summary>
    /// <param name="product">
    /// instance of <see cref="Product"/> class to be used
    /// to generate instance of <see cref="ProductDetailedModel"/>.
    /// </param>
    /// <returns>Returns new instance of <see cref="ProductDetailedModel"/>.</returns>
    internal static ProductDetailedModel ToDetailedModel(this Product product)
    {
        return new ProductDetailedModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Available = product.Available,
            Description = product.Description,
            DateCreated = product.DateCreated
        };
    }

    /// <summary>
    /// Extension method transform instance of <see cref="Product"/> to
    /// <see cref="ProductListItemModel"/> copying all necessary properies.
    /// </summary>
    /// <param name="product">
    /// instance of <see cref="Product"/> class to be used
    /// to generate instance of <see cref="ProductListItemModel"/>.
    /// </param>
    /// <returns>Returns new instance of <see cref="ProductListItemModel"/>.</returns>
    internal static ProductListItemModel ToListItemModel(this Product product)
    {
        return new ProductListItemModel()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Available = product.Available
        };
    }

    /// <summary>
    /// Extension method to transform <see cref="IEnumerable{T}"/> collection of <see cref="Product"/> to
    /// <see cref="IEnumerable{T}"/> collection of <see cref="ProductListItemModel"/> copying all necessary properies.
    /// </summary>
    /// <param name="products">
    /// collection of instances of <see cref="Product"/> to be transformed
    /// </param>
    /// <returns>Returns new <see cref="IEnumerable{T}"/> collection <see cref="ProductListItemModel"/>.</returns>
    internal static IEnumerable<ProductListItemModel> ToListItemModel(this IEnumerable<Product> products)
    {
        if(products == null)
            yield break;
        
        foreach (Product product in products)
            yield return product.ToListItemModel();
    }

}