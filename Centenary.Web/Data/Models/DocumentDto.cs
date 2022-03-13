﻿namespace Centenary.Web.Data.Models;

public class DocumentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; } 
    public int? FolderId { get; set; }  
    public string UploadedBy { get; set; } = "";
    public DateTime UploadedOn { get; set; }
    
    public FolderDto? Folder { get; set; }
}