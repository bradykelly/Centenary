using System.Collections;
using Centenary.Storage;
using Centenary.Web.Data.Models;

namespace Centenary.Web.Models.DocTree;

/// <summary>
/// Models a folder and document tree structure.
/// </summary>
/// <remarks>
/// Uses a disk based folder structure and blob storage to store the documents.</remarks>
public class DocumentTree
{
    readonly IEqualityComparer<Folder> _folderEqualityComparer = new Folder.EqualityComparer();
    readonly IEqualityComparer<Document> _documentEqualityComparer = new Document.EqualityComparer();

    // Initialize folders with one root folder that has no name or parent.
    public HashSet<Folder> Folders { get; } 
    public HashSet<Document> Documents { get; }

    public Folder Root => Folders.Single(f => f.Name == "" && f.ParentPath == "");

    public DocumentTree()
    {
        // Initialize folders with one root folder that has no name or parent.
        Folders = new HashSet<Folder>(_folderEqualityComparer){new()};
        Documents = new HashSet<Document>(_documentEqualityComparer);
    }
}