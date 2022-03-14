namespace Centenary.Web.Models.DocTree;

public class Folder
{
    public int Id { get; set; }
    
    public string Name { get; set; } = "";
    
    public string ParentPath { get; set; } = "";
    
    public List<Document> Documents { get; set; } = new List<Document>();
}