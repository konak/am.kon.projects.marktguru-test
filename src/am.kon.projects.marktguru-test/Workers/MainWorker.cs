using am.kon.projects.marktguru_test.product.abstraction;
using am.kon.projects.marktguru_test.product.business_logic;
using am.kon.projects.marktguru_test.product.common.Services;

namespace am.kon.projects.marktguru_test.Workers;

/// <summary>
/// Main worker responsible for services init, start, stop functionality if they need
/// </summary>
public class MainWorker : BackgroundService
{
    private readonly ILogger<MainWorker> _logger;
    private readonly IConfiguration _configuration;

    private readonly IProductStorage _productStorage;
    private readonly ProductManagementService _productManagementService;
    
    public MainWorker(
        ILogger<MainWorker> logger,
        IConfiguration configuration,
        IProductStorage productStorage,
        ProductManagementService productManagementService
    )
    {
        _logger = logger;
        _configuration = configuration;

        _productStorage = productStorage;
        _productManagementService = productManagementService;
    }

    /// <summary>
    /// Ovveriding ExecuteAsync method to implement periodical checking of status
    /// of necessary services and helth status of the system.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token used to detect whether cancellation was requested from upper level.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Implement some services status periodic checking

            await Task.Delay(1000);
        }
    }

    /// <summary>
    /// Overriding StartAsync to run Start methods of services to init them if necessary.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token provided from upper level</param>
    /// <returns></returns>
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        List<Task> startTasks = new List<Task>();

        startTasks.Add(((IProductService)_productStorage).Start());
        startTasks.Add(_productManagementService.Start());
        
        startTasks.Add(base.StartAsync(cancellationToken));
        
        return Task.WhenAll(startTasks);
    }

    /// <summary>
    /// Overriding StopAsync method to notify services about stop request from upper level (from system) 
    /// </summary>
    /// <param name="cancellationToken">Cancellation token provided from upper level</param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        List<Task> stopTasks = new List<Task>();

        stopTasks.Add(((IProductService)_productStorage).Stop());
        stopTasks.Add(_productManagementService.Stop());

        stopTasks.Add(base.StopAsync(cancellationToken));

        return Task.WhenAll(stopTasks);
    }
}