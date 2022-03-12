using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Centenary.Storage;

public class BlobApiClient : IBlobApiClient
{
    private readonly IConfiguration _configuration;
    public static char PathDelimiter { get; private set; }
    
    public BlobApiClient(IConfiguration configuration)
    {
        _configuration = configuration;
        // The '/' is Azure's default for path delimiters. I want to kept it at one char so I ignore the rest of the configured delimiter string.
        PathDelimiter = string.IsNullOrWhiteSpace(_configuration["Azure:Blobs:PathDelimiter"]) ? '/' : _configuration["Azure:Blobs:PathDelimiter"][0];
    }

    public async Task UploadBlob(string containerName, string blobName, string filePath, string? prefix = null, CancellationToken cancellationToken = default)
    {
        var blobServiceClient = GetServiceClient();
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobPath = $"{prefix?.Trim(PathDelimiter)}{PathDelimiter}{blobName}";
        await using var fileStream = File.OpenRead(filePath);
        await containerClient.UploadBlobAsync(blobPath, fileStream, cancellationToken);
    }

    public async Task<List<string>> GetBlobNamesByHierarchy(string containerName, string? prefix = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException($"{nameof(containerName)} is required", nameof(containerName));
        }

        var retList = new List<string>();
        var blobServiceClient = GetServiceClient();
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        async Task GetBlobNamesRecursive(string? currentPrefix = null)
        {
            await foreach (BlobHierarchyItem blobItem in containerClient.GetBlobsByHierarchyAsync(prefix: currentPrefix, delimiter: PathDelimiter.ToString(), cancellationToken: cancellationToken))
            {
                retList.Add(blobItem.IsPrefix ? blobItem.Prefix : blobItem.Blob.Name);
                if (blobItem.IsPrefix)
                {
                    await GetBlobNamesRecursive(blobItem.Prefix);
                }
            }
        }

        await GetBlobNamesRecursive(prefix);

        return retList;
    }

    private BlobServiceClient GetServiceClient()
    {
        return new BlobServiceClient(_configuration["Azure:Storage:ConnectionString"]);
    }
}