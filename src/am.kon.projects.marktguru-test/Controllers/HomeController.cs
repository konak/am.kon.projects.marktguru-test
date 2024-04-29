using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using am.kon.projects.marktguru_test.Models;

namespace am.kon.projects.marktguru_test.Controllers;

[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Client)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}