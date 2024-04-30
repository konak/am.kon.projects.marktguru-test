namespace am.kon.projects.marktguru_test.Models.Products;

/// <summary>
/// Product model used in the web page view or in API call response
/// where list of products is requested 
/// </summary>
public class ProductListItemModel : ProductBaseModel
{
    /// <summary>
    /// Name of the product.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Price of the product.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Property identifying whether product is available or not.
    /// </summary>
    public bool Available { get; set; }
}