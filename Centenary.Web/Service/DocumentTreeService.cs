using Centenary.Storage;
using Centenary.Web.Data;
using Centenary.Web.Models.DocTree;

namespace Centenary.Web.Service;

public class DocumentTreeService
{
    private const string ArchiveContainerName = "archive";
    private readonly IBlobApiClient _blobApiClient;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DocumentTreeService> _logger;
    private readonly char _pathDelimiter;

    public DocumentTreeService(IBlobApiClient blobApiClient, ApplicationDbContext dbContext, ILogger<DocumentTreeService> logger)
    {
        _blobApiClient = blobApiClient;
        _dbContext = dbContext;
        _logger = logger;
        _pathDelimiter = _blobApiClient.PathDelimiter;
    }
    
    /// <summary>
    /// Synchronizes the root  folder in a document tree model with the blob storage and the local folder tree.
    /// </summary>
    public async Task<DocumentTree> IndexBlobs()
    {
        var tree = new DocumentTree();
        
        var blobNames = await _blobApiClient.GetBlobNames(ArchiveContainerName);
        var blobPaths = blobNames.Select(n => n.Split(_pathDelimiter)).OrderBy(p => p.Length).ToList();

        foreach (var path in blobPaths)
        {
            if (path.Length == 1)
            {
                var doc = new Document();
                doc.Name = path[0];
                doc.Folder = tree.Root;
                doc.CreatedBy = "System";
                doc.CreatedOn = DateTime.Now;
            }
        }
        
        return tree;
    }    
}