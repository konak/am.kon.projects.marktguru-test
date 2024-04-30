namespace am.kon.projects.marktguru_test.product.common.Action;

public class ProductActionResult<T>
{
    /// <summary>
    /// Result of the action.
    /// </summary>
    public ProductActionResultTypes ActionResult { get; set; }
    
    /// <summary>
    /// Message returned from the action.
    /// </summary>
    public string? Message { get; set; }
    
    /// <summary>
    /// Exception returned from action in case of action failure.
    /// </summary>
    public Exception? Exception { get; set; }
    
    /// <summary>
    /// Data returned from action in case of successfull execution.
    /// </summary>
    public T? Data { get; set; }
}