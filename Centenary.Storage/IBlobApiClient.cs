namespace Centenary.Storage;

public interface IBlobApiClient
{
    Task<List<string>> GetPictureNames();
}