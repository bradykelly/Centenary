﻿namespace Centenary.Api.Data.Models;

public class DocumentDto
{
    public int Id { get; set; }
    public string FullPath { get; set; } = "";
    public string? Description { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime CreatedOn { get; set; }
}