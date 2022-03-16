namespace Centenary.Web.Models.DocTree;

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
            return x.Name.Equals(y.Name, StringComparison.InvariantCulture);
        }

        public int GetHashCode(Document obj)
        {
            return obj.Id.GetHashCode();
        }
    }
    
    public int Id { get; set; }
    
    public string Name { get; set; } = "";
    
    public Folder Folder { get; set; } = new Folder();
    
    public string? Description { get; set; }
    
    public string CreatedBy { get; set; } = "System";
    
    public DateTime CreatedOn { get; set; }
}