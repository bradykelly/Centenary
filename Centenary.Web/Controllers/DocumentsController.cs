using Centenary.Web.Models;
using Centenary.Web.Service;
using Centenary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Centenary.Web.Controllers;

public class DocumentsController : Controller
{
    private readonly IDocumentService _treeService;
    private readonly ILogger<DocumentsController> _logger;
    private readonly BlobOptions _options;

    public DocumentsController(IDocumentService treeService, ILogger<DocumentsController> logger, IOptions<BlobOptions> options)
    {
        _treeService = treeService;
        _logger = logger;
        _options = options.Value;
    }
    
    // GET
    public async Task<IActionResult> Documents(string folderPath = "")
    {
        var tree = await _treeService.IndexBlobs();
        var model = new DocTreeViewModel();
        model.ParentPath = string.IsNullOrWhiteSpace(folderPath) ? "/" : folderPath;
        model.Folders = tree.Folders
            .Where(f => f.ParentPath == folderPath)
            .OrderBy(f => f.Name);
        model.Documents = tree.Documents
            .Where(d =>  d.Name != _options.EmptyFolderFilename && d.FolderPath.Equals(folderPath, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(d => d.Name);
        
        return View(model);
    }
}