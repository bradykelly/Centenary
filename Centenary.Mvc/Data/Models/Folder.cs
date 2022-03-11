namespace Centenary.Mvc.Data.Models;

public class Folder
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }

    public Folder? Parent { get; set; }
    public ICollection<Folder> Folders { get; set; } = new List<Folder>();
}