namespace am.kon.projects.marktguru_test.product.common.Action;

/// <summary>
/// Enum describing possible result types of action of a product
/// </summary>
public enum ProductActionResultTypes
{
    /// <summary>
    /// Unknown result of the action. Something gone wrong and result was not set.
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Action completed successfully.
    /// </summary>
    Ok = 1,
    
    /// <summary>
    /// There was an error on action.
    /// </summary>
    Error = 2,
    
    /// <summary>
    /// A case when the action was not completed and sending back an information
    /// for user confirmation or any other purposes.
    /// </summary>
    Info = 3
}