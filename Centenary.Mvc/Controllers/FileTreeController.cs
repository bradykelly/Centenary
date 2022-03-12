using Centenary.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Centenary.Mvc.Controllers;

public class FileTreeController : Controller
{
    private readonly IBlobApiClient _blobApiClient;
    private readonly ILogger<FileTreeController> _logger;

    public FileTreeController(IBlobApiClient blobApiClient, ILogger<FileTreeController> logger, ILogger<FileTreeController> logger1)
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