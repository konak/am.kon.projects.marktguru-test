namespace am.kon.projects.marktguru_test.Controllers.Models;

/// <summary>
/// Base model used to show data on Products controller pages 
/// </summary>
public class ProductsPageBaseModel<T>
{
    /// <summary>
    /// Property identifying whether model contains message.
    /// </summary>
    public bool HasMessage
    {
        get { return string.IsNullOrEmpty(Message); }
    }
    
    /// <summary>
    /// Message to be shown on the view
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Model of a data to be shown on the page 
    /// </summary>
    public T Data { get; set; }
}