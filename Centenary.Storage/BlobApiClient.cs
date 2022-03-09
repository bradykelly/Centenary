using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Centenary.Storage;

public class BlobApiClient : IBlobApiClient
{
    private readonly IConfiguration _configuration;
    private readonly char _pathDelimiter;
    
    public BlobApiClient(IConfiguration configuration)
    {
        _configuration = configuration;
        // The '/' is Azure's default for path delimiters. I want to kept it at one char so I ignore the rest of the configured delimiter string.
        _pathDelimiter = string.IsNullOrWhiteSpace(_configuration["Azure:Blobs:PathDelimiter"]) ? '/' : _configuration["Azure:Blobs:PathDelimiter"][0];
    }

    public async Task UploadBlob(string containerName, string blobName, string filePath, string? prefix = null, CancellationToken cancellationToken = default)
    {
        var blobServiceClient = GetServiceClient();
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobPath = !string.IsNullOrWhiteSpace(prefix?.Trim(_pathDelimiter)) ? $"{prefix.Trim('/')}{_pathDelimiter}{blobName}" : blobName;
        await using var fileStream = File.OpenRead(filePath);
        await containerClient.UploadBlobAsync(blobPath, fileStream, cancellationToken);
    }
    
    public async Task<List<string>> GetBlobNames(string containerName, string? prefix = null, bool foldersOnly = false, CancellationToken cancellationToken = default)
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
            await foreach (BlobHierarchyItem blobItem in containerClient.GetBlobsByHierarchyAsync(prefix: currentPrefix, delimiter: _pathDelimiter.ToString(), cancellationToken: cancellationToken))
            {
                // BKNB I must check if this will work for top level folders only even when prefix is not null. Then it should return only 1 layer of folders.
                if (foldersOnly && !blobItem.IsPrefix)
                {
                    continue;
                }
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