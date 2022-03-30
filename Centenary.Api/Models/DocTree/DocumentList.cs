namespace Centenary.Api.Models.DocTree;

/// <summary>
/// Models a folder and document tree structure.
/// </summary>
/// <remarks>
/// Uses a disk based folder structure and blob storage to store the documents.</remarks>
public class DocumentList
{
    readonly IEqualityComparer<Folder> _folderEqualityComparer = new Folder.EqualityComparer();
    readonly IEqualityComparer<Document> _documentEqualityComparer = new Document.EqualityComparer();

    // Initialize folders with one root folder that has no name or parent.
    public HashSet<Folder> Folders { get; } 
    public HashSet<Document> Documents { get; }

    public DocumentList()
    {
        Folders = new HashSet<Folder>(_folderEqualityComparer);
        Documents = new HashSet<Document>(_documentEqualityComparer);
    }
}