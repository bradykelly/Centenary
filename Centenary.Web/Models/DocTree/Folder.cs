using System.Runtime.InteropServices;

namespace Centenary.Web.Models.DocTree;

public class Folder
{
    public class EqualityComparer : IEqualityComparer<Folder>
    {
        public bool Equals(Folder? x, Folder? y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            if (x is null || y is null)
            {
                return false;
            }
            return x.FullPath.Equals(y.FullPath, StringComparison.InvariantCulture);
        }

        public int GetHashCode(Folder obj)
        {
            return obj.Name.GetHashCode();
        }
    }
    
    private readonly Document.EqualityComparer _documentEqualityComparer = new();
    
    public int Id { get; set; }

    public string FullPath { get; set; } = "";
    
    public string Name => string.IsNullOrWhiteSpace(FullPath) ? "" : Path.GetFileName(FullPath);
    
    public string ParentPath => string.IsNullOrWhiteSpace(FullPath) ? "" : Path.GetDirectoryName(FullPath) ?? "";

    public HashSet<Document> Documents { get; }

    public Folder()
    {
        Documents = new HashSet<Document>(_documentEqualityComparer);
    }
}