using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;

namespace Centenary.Mvc.Data.Models;

public class Document
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; } 
    // BKTODO Later on make this an FK into the Folders table
    public string? Folder { get; set; }
    public string UploadedBy { get; set; } = "";
    public DateTime UploadedOn { get; set; }
}