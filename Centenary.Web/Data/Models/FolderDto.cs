namespace Centenary.Web.Data.Models;

public class FolderDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public FolderDto? Parent { get; set; }
    
    public ICollection<FolderDto> Folders { get; set; } = new List<FolderDto>();
    public ICollection<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
}