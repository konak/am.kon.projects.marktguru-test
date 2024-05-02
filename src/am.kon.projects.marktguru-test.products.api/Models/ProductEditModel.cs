using System.ComponentModel.DataAnnotations;

namespace am.kon.projects.marktguru_test.products.api.Models;

/// <summary>
/// Model used to update product data in the storage
/// </summary>
public class ProductEditModel
{
    /// <summary>
    /// Id of the product
    /// </summary>
    [Required]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Name of the product.
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Price of the product.
    /// </summary>
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
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
    /// Version of last update of the product.
    /// </summary>
    public int Version { get; set; }
}