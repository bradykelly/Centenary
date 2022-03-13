using System.Diagnostics;
using Centenary.Storage;
using Centenary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Centenary.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlobApiClient _blobApiClient;

    public HomeController(ILogger<HomeController> logger, IBlobApiClient blobApiClient)
    {
        _logger = logger;
        _blobApiClient = blobApiClient;
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