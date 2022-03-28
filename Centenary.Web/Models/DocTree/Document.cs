﻿namespace Centenary.Web.Models.DocTree;

public class Document
{
    public class EqualityComparer : IEqualityComparer<Document>
    {
        public bool Equals(Document? x, Document? y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            if (x is null || y is null)
            {
                return false;
            }
            return x.Name.Equals(y.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(Document obj)
        {
            return obj.FullPath.GetHashCode();
        }
    }
    
    public string Name => Path.GetFileName(FullPath);
    
    public string FolderPath => Path.GetDirectoryName(FullPath) ?? string.Empty;

    public string FullPath { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; }
}