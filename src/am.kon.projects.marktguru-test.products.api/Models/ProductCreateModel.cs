using System.ComponentModel.DataAnnotations;

namespace am.kon.projects.marktguru_test.products.api.Models;

// todo
// This class has a duplicate implementation in am.kon.projects.marktguru_test project
// move to a shared library should be done

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
}