namespace Centenary.Mvc.Models.DocTree;

public interface IDocumentTreeItem
{
    public string Name { get; set; }
    
    public string FolderPrefix { get; set; }
    
    public string CreatedBy { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
}