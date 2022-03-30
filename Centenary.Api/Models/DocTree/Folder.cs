namespace Centenary.Api.Models.DocTree;

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
            return x.FullPath.Equals(y.FullPath, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(Folder obj)
        {
            return obj.Name.GetHashCode();
        }
    }
    
    public int Id { get; set; }

    public string FullPath { get; set; } = string.Empty;
    
    public string Name => Path.GetFileName(FullPath);
    
    public string ParentPath => Path.GetDirectoryName(FullPath) ?? string.Empty;
}