using Centenary.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Centenary.Mvc.Controllers;

public class DocTreeController : Controller
{
    private readonly IBlobApiClient _blobApiClient;
    private readonly ILogger<DocTreeController> _logger;

    public DocTreeController(IBlobApiClient blobApiClient, ILogger<DocTreeController> logger, ILogger<DocTreeController> logger1)
    {
        _blobApiClient = blobApiClient;
        _logger = logger1;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
}