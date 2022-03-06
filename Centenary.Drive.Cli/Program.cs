using System.Windows.Input;
using Centenary.Drive.Cli;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IDriveApiClient, DriveApiClient>();
    })
    .Build();

var service = host.Services.GetRequiredService<IDriveApiClient>();
await service.CreateSharedDrive("spinks");
//var check = files;