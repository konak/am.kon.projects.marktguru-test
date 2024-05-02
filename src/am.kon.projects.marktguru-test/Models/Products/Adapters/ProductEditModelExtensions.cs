using am.kon.projects.marktguru_test.product.common.Models;
using Microsoft.AspNetCore.Http.Connections;

namespace am.kon.projects.marktguru_test.Models.Products.Adapters;

/// <summary>
/// Class for extension methods of <see cref="ProductEditModel"/>
/// </summary>
public static class ProductEditModelExtensions
{
    /// <summary>
    /// Extension method for instance of <see cref="ProductEditModel"/> generating an instance of <see cref="Product"/>
    /// </summary>
    /// <param name="productEditModel"></param>
    /// <returns></returns>
    public static Product ToProduct(this ProductEditModel productEditModel)
    {
        return new Product
        {
            Id = productEditModel.Id,
            Name = productEditModel.Name,
            Price = productEditModel.Price,
            Available = productEditModel.Available,
            Description = productEditModel.Description,
            Version = productEditModel.Version
        };
    }

    /// <summary>
    /// Extension method transforming instance of <see cref="Product"/>
    /// to an instance of <see cref="ProductEditModel"/>.
    /// </summary>
    /// <param name="product">Instance of <see cref="Product"/> to be converted.</param>
    /// <returns>Returns an instance of <see cref="ProductEditModel"/>.</returns>
    public static ProductEditModel ToProductEditModel(this Product product)
    {
        return new ProductEditModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Available = product.Available,
            Description = product.Description,
            Version = product.Version
        };
    }
}