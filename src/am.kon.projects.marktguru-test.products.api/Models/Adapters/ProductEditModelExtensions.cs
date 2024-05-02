using am.kon.projects.marktguru_test.product.common.Models;

namespace am.kon.projects.marktguru_test.products.api.Models.Adapters;


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
}