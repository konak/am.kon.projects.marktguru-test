namespace am.kon.projects.marktguru_test.Models.Products;

/// <summary>
/// Model of the product used on pages or in API calls
/// when detailed information of the product is requested.
/// </summary>
public class ProductDetailedModel : ProductBaseModel
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
    
    /// <summary>
    /// Description of the product.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Date the product was created in the system.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Version of last update of the product.
    /// </summary>
    public int Version { get; set; }
}