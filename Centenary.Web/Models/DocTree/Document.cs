namespace Centenary.Web.Models.DocTree;

public class Document
{
    public int Id { get; set; }
    
    public string Name { get; set; } = "";
    
    public Folder Folder { get; set; } = new Folder();
    
    public string? Description { get; set; }
    
    public string CreatedBy { get; set; } = "System";
    
    public DateTime CreatedOn { get; set; }
}