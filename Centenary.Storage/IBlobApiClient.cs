namespace Centenary.Storage;

public interface IBlobApiClient
{
    char PathDelimiter { get; }

    // BKTODO Create overloads that take a path tree or folder id instead of a prefix, convert path to prefix and call the prefix overloads
    
    /// <summary>
    /// Uploads a file to a blob in the specified container.
    /// </summary>
    /// <param name="containerName">The name of the container to upload into.</param>
    /// <param name="blobName">The name of the blob to upload to.</param>
    /// <param name="filePath">The full path of the file to upload.</param>
    /// <param name="prefix">A prefix i.e. folder path under which to save the blob</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>passed to methods on the Azure storage API.</param>
    /// <returns></returns>
    Task UploadBlob(string containerName, string blobName, string filePath, string? prefix = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Recursively gets a list of blobs in the specified container.
    /// </summary>
    /// <param name="containerName">The name of the container to list blobs for.</param>
    /// <param name="prefix">Blob name/path Prefix under which to list blobs.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>passed to methods on the Azure storage API.</param>
    /// <returns>A list of full blob paths i.e. prefixes and names for each blob in the container named <paramref name="containerName"/>.</returns>
    Task<List<string>> GetBlobNamesByHierarchy(string containerName, string? prefix = null, CancellationToken cancellationToken = default);

    // BKTODO Document these
    
    public Task<List<string>> GetFolderNames(string containerName, string? prefix = null, CancellationToken cancellationToken = default);
    
    public Task<List<string>> GetBlobNames(string containerName, string? prefix = null, CancellationToken cancellationToken = default);
    
}