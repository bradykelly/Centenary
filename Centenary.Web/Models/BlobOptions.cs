namespace Centenary.Web.Models;

public class BlobOptions
{
    public string PathDelimiter { get; set; } = string.Empty;
    public string DefaultContainer { get; set; } = "archive";
    public string EmptyFolderFilename { get; set; } = "__empty__.txt";
    public string SystemUserName { get; set; } = "System";
}