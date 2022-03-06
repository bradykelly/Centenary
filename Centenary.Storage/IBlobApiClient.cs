namespace Centenary.Storage;

public interface IBlobApiClient
{
    Task<List<string>> GetBlobNames(string containerName);
}