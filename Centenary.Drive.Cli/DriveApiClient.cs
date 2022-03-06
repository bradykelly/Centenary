using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Centenary.Drive.Cli;

public class DriveApiClient : IDriveApiClient
{
    const string JsonKeyPath = @"ApiAccountKey\sphynx-drive-170cc41754a5.json";

    private DriveService GetDriveService()
    {
        var credential = GoogleCredential.FromFile(JsonKeyPath)
            .CreateScoped(new[] { DriveService.Scope.Drive });
        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential
        });
        return service;
    }   
    
    public async Task CreateSharedDrive(string driveName)
    {
        var requestId = Guid.NewGuid();
        var service = GetDriveService();
        var drive = new Google.Apis.Drive.v3.Data.Drive();
        drive.Kind = "drive#drive";
        drive.Name = driveName;
        var request = service.Drives.Create(drive, requestId.ToString());
        request.Fields = "id";
        var response = await request.ExecuteAsync();
    }
    
    //public void UploadFile(string filePath)
    
    public async Task<IEnumerable<string>> ListFiles()
    {
        var service = GetDriveService();
        var request = service.Files.List();
        
        request.PageSize = 1000;
        
        var results = await request.ExecuteAsync();
        var files = results.Files;
        return files.Select(f => f.Name);
    }
    
}