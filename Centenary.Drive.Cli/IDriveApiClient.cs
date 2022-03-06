namespace Centenary.Drive.Cli;

public interface IDriveApiClient
{
    Task CreateSharedDrive(string driveName);
    Task<IEnumerable<string>> ListFiles();
}