using Centenary.Storage;

namespace Centenary.Web.Models.DocTree;

/// <summary>
/// Models a folder and document tree structure.
/// </summary>
/// <remarks>
/// Uses a disk based folder structure and blob storage to store the documents.</remarks>
public class DocumentTree
{
    public Folder Root { get; set; } = new Folder();
}