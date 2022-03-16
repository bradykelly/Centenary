using Centenary.Storage;
using Centenary.Web.Data;
using Centenary.Web.Models.DocTree;

namespace Centenary.Web.Service;

public interface IDocumentTreeService
{
    /// <summary>
    /// Indexes the specified container into a new <see cref="DocumentTree"/>.
    /// </summary>
    Task<DocumentTree> IndexBlobs(string containerName, string username = "System");
}

public class DocumentTreeService : IDocumentTreeService
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
    /// Indexes the specified container into a new <see cref="DocumentTree"/>.
    /// </summary>
    public async Task<DocumentTree> IndexBlobs(string containerName, string username = "System")
    {
        var tree = new DocumentTree();
        
        // Call without a prefix to get all blobs in the container with their full paths as prefixes.
        var blobNames = await _blobApiClient.GetBlobNamesByPrefix(containerName);
        var blobPaths = blobNames.Select(n => n.Split(_pathDelimiter)).OrderBy(p => p.Length).ToList();

        foreach (var path in blobPaths)
        {
            if (path.Length == 1)
            {
                // The path has no prefix and is therefore a document in the root folder.
                var doc = new Document
                {
                    Name = path[0],
                    Folder = tree.Root,
                    CreatedBy = username,
                    CreatedOn = DateTime.Now
                };

                tree.Documents.Add(doc);
            }
            else
            {
                var fullFolderPath = Path.GetDirectoryName(string.Join(_pathDelimiter.ToString(), path))!;
                var docName = Path.GetFileName(string.Join(_pathDelimiter.ToString(), path));

                var folder = new Folder { FullPath = fullFolderPath };
                if (!tree.Folders.Contains(folder))
                {
                    tree.Folders.Add(folder);
                }

                var doc = new Document
                {
                    Name = docName,
                    Folder = folder,
                    CreatedBy = username,
                    CreatedOn = DateTime.Now
                };
                tree.Documents.Add(doc);
            }
        }
        
        return tree;
    }    
}