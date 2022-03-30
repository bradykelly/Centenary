using Centenary.Api.Data;
using Centenary.Api.Models;
using Centenary.Api.Models.DocTree;
using Centenary.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Centenary.Api.Service;

public interface IDocumentService
{
    /// <summary>
    /// Indexes the configured container into a new <see cref="DocumentList"/>.
    /// </summary>
    /// <param name="prefix"></param>
    Task<DocumentList> IndexBlobs(string prefix = "");
}

public class DocumentService : IDocumentService
{
    private const string ArchiveContainerName = "archive";
    private readonly IBlobApiClient _blobApiClient;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DocumentService> _logger;
    private readonly BlobOptions _options;

    public DocumentService(IBlobApiClient blobApiClient, ApplicationDbContext dbContext, ILogger<DocumentService> logger, IOptions<BlobOptions> blobOptions)
    {
        _blobApiClient = blobApiClient;
        _dbContext = dbContext;
        _logger = logger;
        _options = blobOptions.Value;
    }

    /// <summary>
    /// Indexes the specified container into a new <see cref="DocumentList"/>.
    /// </summary>
    /// <param name="prefix"></param>
    public async Task<DocumentList> IndexBlobs(string prefix = "")
    {
        var docs = new DocumentList();

        // Call without a prefix to get all blobs in the container with their full paths.
        var allBlobNames = await _blobApiClient.GetBlobNamesByPrefix(_options.DefaultContainer, prefix);

        foreach (var blobName in allBlobNames)
        {
            var folder = new Folder { FullPath = Path.GetDirectoryName(blobName) ?? string.Empty };
            if (!string.IsNullOrWhiteSpace(folder.FullPath) && !docs.Folders.Contains(folder))
            {
                docs.Folders.Add(folder);
            }

            var doc = new Document
            {
                FullPath = blobName
            };
            var dto = await _dbContext.Documents.FirstOrDefaultAsync(d => d.FullPath == blobName);
            if (dto == null)
            {
                doc.CreatedBy = _options.SystemUserName;
                doc.CreatedOn = DateTime.Now;
            }
            else
            {
                doc.Description = dto.Description;
                doc.CreatedBy = dto.CreatedBy;
                doc.CreatedOn = dto.CreatedOn;
            }

            docs.Documents.Add(doc);
        }

        return docs;
    }
}