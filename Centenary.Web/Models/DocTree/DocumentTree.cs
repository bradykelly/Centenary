using Centenary.Storage;

namespace Centenary.Web.Models.DocTree;

/// <summary>
/// Models a folder and document tree.
/// </summary>
/// <remarks>
/// Uses a disk based folder structure and blob storage to store the documents. </remarks>
public class DocumentTree
{
    private const string ArchiveContainerName = "archive";
    private char _pathDelimiter;
    
    private readonly IBlobApiClient _apiClient;

    public DocumentTree(IBlobApiClient apiClient)
    {
        _apiClient = apiClient;
        _pathDelimiter = _apiClient.PathDelimiter;
    }

    /// <summary>
    /// Synchronizes a folder in a document tree model with the blob storage and the local folder tree.
    /// </summary>
    /// <param name="parentPath">The absolute path to the parent folder to index.</param>
    public async Task Index(string parentPath)
    {
        var folders = await _apiClient.GetFolderNames(ArchiveContainerName);
        
        foreach(var folder in folders)
        {
        
        }
    }
}