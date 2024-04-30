using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace am.kon.projects.marktguru_test.product.common.Services;

/// <summary>
/// Base service for all services of products
/// </summary>
public class ProductServiceBase : IProductService
{
    protected readonly ILogger<ProductServiceBase> _logger;
    protected readonly IConfiguration _configuration;
    protected readonly CancellationTokenSource _cancellationTokenSource;
    protected readonly CancellationToken _cancellationToken;
    
    public ProductServiceBase(
        ILogger<ProductServiceBase> logger,
        IConfiguration configuration
        )
    {
        _logger = logger;
        _configuration = configuration;
        
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }

    /// <summary>
    /// Method to start service.
    /// </summary>
    /// <returns></returns>
    public virtual Task Start()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Method to stop the service
    /// </summary>
    /// <returns></returns>
    public virtual Task Stop()
    {
        return _cancellationTokenSource.CancelAsync();
    }
}