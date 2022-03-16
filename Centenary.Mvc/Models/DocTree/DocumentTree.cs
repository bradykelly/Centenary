using Centenary.Storage;

namespace Centenary.Mvc.Models.DocTree;

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
}