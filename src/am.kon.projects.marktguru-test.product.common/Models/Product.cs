namespace am.kon.projects.marktguru_test.product.common.Models;

public class Product
{
    /// <summary>
    /// Id of the product.
    /// </summary>
    public Guid Id { get; set; }
    
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