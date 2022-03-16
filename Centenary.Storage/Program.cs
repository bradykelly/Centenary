using Centenary.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddTransient<IBlobApiClient, BlobApiClient>(); })
    .Build();

const string filePath = @"D:\Personal\Dotnet Projects\Centenary\Centenary.Files\Folder 2\sphynx bye laws.PDF";
var client = host.Services.GetRequiredService<IBlobApiClient>();
var names = await client.GetBlobNamesByPrefix("archive");
var check = names;