using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Centenary.Storage;

public class BlobApiClient : IBlobApiClient
{
    private readonly IConfiguration _configuration;
    
    public BlobApiClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<string>> GetPictureNames()
    {
        var retList = new List<string>();
        var blobServiceClient = GetServiceClient();
        var containerClient = blobServiceClient.GetBlobContainerClient("pictures");
        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        {
            retList.Add(blobItem.Name);
        }

        return retList;
    }

    private BlobServiceClient GetServiceClient()
    {
        return new BlobServiceClient(_configuration["Azure:Storage:ConnectionString"]);
    }
}