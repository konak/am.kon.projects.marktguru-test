using System.ComponentModel.DataAnnotations;

namespace am.kon.projects.marktguru_test.Models.Products;

/// <summary>
/// Model used to create product record in storage.
/// </summary>
public class ProductCreateModel
{
    /// <summary>
    /// Name of the product.
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Price of the product.
    /// </summary>
    [Required]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Property identifying whether product is available or not.
    /// </summary>
    public bool Available { get; set; }
    
    /// <summary>
    /// Description of the product.
    /// </summary>
    public string? Description { get; set; }
}