namespace Centenary.Web.Data.Models;

public class FolderDto
{
    public int Id { get; set; }
    public string Path { get; set; } = null!;
    public virtual ICollection<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
}