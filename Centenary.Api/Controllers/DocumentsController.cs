using System.Net.Mime;
using Centenary.Api.Models;
using Centenary.Api.Service;
using Centenary.Models.DocTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Centenary.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
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
    
    [HttpGet]
    [Route("")]
    public async Task<DocumentList> GetDocuments()
    {
        return await GetDocuments("");
    }

    [HttpGet]
    [Route("{folderPath}")]
    public async Task<DocumentList> GetDocuments(string folderPath)
    {
        var tree = await _treeService.IndexBlobs(folderPath);
        return tree;
    }
}