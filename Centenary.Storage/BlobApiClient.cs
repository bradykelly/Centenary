using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Centenary.Storage;

public class BlobApiClient : IBlobApiClient
{
    private readonly IConfiguration _configuration;
    private readonly string _pathDelimiter = "/";
    
    public BlobApiClient(IConfiguration configuration)
    {
        _configuration = configuration;
        _pathDelimiter = _configuration["Azure:Blobs:PathDelimiter"] ?? "/";
    }

    public async Task<List<string>> GetBlobNames()
    {
        var retList = new List<string>();
        var blobServiceClient = GetServiceClient();
        var containerClient = blobServiceClient.GetBlobContainerClient("archive");

        async Task GetBlobNamesRecursive(string? prefix = null)
        {
            await foreach (BlobHierarchyItem blobItem in containerClient.GetBlobsByHierarchyAsync(prefix: prefix, delimiter: _pathDelimiter))
            {
                retList.Add(blobItem.IsPrefix ? blobItem.Prefix : blobItem.Blob.Name);
                if (blobItem.IsPrefix)
                {
                    await GetBlobNamesRecursive(blobItem.Prefix);
                }
            }
        }

        await GetBlobNamesRecursive();

        return retList;
    }

    private BlobServiceClient GetServiceClient()
    {
        return new BlobServiceClient(_configuration["Azure:Storage:ConnectionString"]);
    }
}