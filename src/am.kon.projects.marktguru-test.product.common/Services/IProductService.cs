namespace am.kon.projects.marktguru_test.product.common.Services;

/// <summary>
/// Interface to access service methods of product.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Method to start service.
    /// </summary>
    /// <returns></returns>
    Task Start();

    /// <summary>
    /// Method to stop service.
    /// </summary>
    /// <returns></returns>
    Task Stop();
}