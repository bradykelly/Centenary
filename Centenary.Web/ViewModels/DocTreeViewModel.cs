using Centenary.Web.Models.DocTree;

namespace Centenary.Web.ViewModels;

public class DocTreeViewModel
{
    public string ParentPath { get; set; } = "/";
    
    public IEnumerable<Folder> Folders { get; set; } = new List<Folder>();
    
    public IEnumerable<Document> Documents { get; set; } = new List<Document>();
}