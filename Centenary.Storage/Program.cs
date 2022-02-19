using Centenary.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddTransient<IBlobApiClient, BlobApiClient>(); })
    .Build();

var client = host.Services.GetRequiredService<IBlobApiClient>();
var names = await client.GetPictureNames();
var check = names;