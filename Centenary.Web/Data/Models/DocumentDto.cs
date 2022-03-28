namespace Centenary.Web.Data.Models;

public class DocumentDto
{
    public int Id { get; set; }
    public string FullPath { get; } = "";
    public string? Description { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime CreatedOn { get; set; }
}