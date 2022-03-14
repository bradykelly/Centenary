namespace Centenary.Web.Data.Models;

public class DocumentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string FolderPath { get; } = "";
    public string CreatedBy { get; set; } = "";
    public DateTime CreatedOn { get; set; }
    
    public FolderDto? Folder { get; set; }
}