using Centenary.Web.Service;
using Centenary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Centenary.Web.Controllers;

public class DocTreeController : Controller
{
    private readonly IDocumentTreeService _treeService;
    private readonly ILogger<DocTreeController> _logger;
    private readonly IConfiguration _config;

    private readonly string _anonUser;
    private readonly string _container;

    public DocTreeController(IDocumentTreeService treeService, ILogger<DocTreeController> logger, IConfiguration configuration)
    {
        _treeService = treeService;
        _logger = logger;
        _config = configuration;
        
        _anonUser = _config["Web:Docs:AnonymousUser"];
        _container = _config["Azure:Blobs:DefaultContainer"];
    }
    
    // GET
    public async Task<IActionResult> Folders()
    {
        var tree = await _treeService.IndexBlobs(_container, _anonUser);
        var model = new DocTreeViewModel();
        model.Folders = tree.Folders
            .Where(f => !string.IsNullOrWhiteSpace(f.FullPath) && string.IsNullOrWhiteSpace(f.ParentPath))
            .OrderBy(f => f.Name);
        model.Documents = tree.Documents
            .Where(d => string.IsNullOrWhiteSpace(d.Folder.Name))
            .OrderBy(d => d.Name);
        
        return View(model);
    }
}