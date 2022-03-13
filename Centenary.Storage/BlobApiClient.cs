﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Centenary.Storage;

public class BlobApiClient : IBlobApiClient
{
    private readonly IConfiguration _configuration;
    public char PathDelimiter { get; }

    public BlobApiClient(IConfiguration configuration)
    {
        _configuration = configuration;
        // The '/' is Azure's default for path delimiters. I want to kept it at one char so I ignore the rest of the configured delimiter string.
        PathDelimiter = string.IsNullOrWhiteSpace(_configuration["Azure:Blobs:PathDelimiter"]) ? '/' : _configuration["Azure:Blobs:PathDelimiter"][0];
    }

    /// <inheritdoc cref="IBlobApiClient.UploadBlob" />
    public async Task UploadBlob(string containerName, string blobName, string filePath, string? prefix = null, CancellationToken cancellationToken = default)
    {
        var containerClient = GetContainerClient(containerName);
        var blobPath = $"{prefix?.Trim(PathDelimiter)}{PathDelimiter}{blobName}";
        await using var fileStream = File.OpenRead(filePath);
        await containerClient.UploadBlobAsync(blobPath, fileStream, cancellationToken);
    }

    /// <inheritdoc cref="IBlobApiClient.GetBlobNamesByHierarchy" />
    public async Task<List<string>> GetBlobNamesByHierarchy(string containerName, string? prefix = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException($"{nameof(containerName)} is required", nameof(containerName));
        }

        var retList = new List<string>();
        var containerClient = GetContainerClient(containerName);

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

    public async Task<List<string>> GetFolderNames(string containerName, string? prefix = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException($"{nameof(containerName)} is required", nameof(containerName));
        }
        
        var retList = new List<string>();
        var containerClient = GetContainerClient(containerName);

        await foreach (var blobItem in containerClient.GetBlobsByHierarchyAsync(prefix: prefix, delimiter: PathDelimiter.ToString(), cancellationToken: cancellationToken))
        {
            if (blobItem.IsPrefix)
            {
                retList.Add(blobItem.Prefix.TrimEnd(PathDelimiter));
            }
        }

        return retList;
    }
    
    public async Task<List<string>> GetBlobNames(string containerName, string? prefix = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException($"{nameof(containerName)} is required", nameof(containerName));
        }
        
        var retList = new List<string>();
        var containerClient = GetContainerClient(containerName);
        
        await foreach(var blobItem in containerClient.GetBlobsAsync(prefix: prefix, cancellationToken: cancellationToken))
        {
            // Hard coded path delimiter because GetBlobsAsync doesn't take a delimiter parameter and returns delimiter '/' by default
            if (string.IsNullOrWhiteSpace(prefix) && blobItem.Name.Split('/').Length > 1)
            {
                continue;
            }
            var name = !string.IsNullOrWhiteSpace(prefix) ? blobItem.Name.Replace(prefix!, "") : blobItem.Name;
            retList.Add(name);
        }
        
        return retList;
    }
    
    private BlobContainerClient GetContainerClient(string containerName)
    {
        var blobServiceClient = GetServiceClient();
        return blobServiceClient.GetBlobContainerClient(containerName);
    }
    
    private BlobServiceClient GetServiceClient()
    {
        return new BlobServiceClient(_configuration["Azure:Storage:ConnectionString"]);
    }
}