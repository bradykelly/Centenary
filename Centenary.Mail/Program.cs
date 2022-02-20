using Centenary.Mail.SendInBlue;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddTransient<IEmailSender, SendInBlueApiClient>(); })
    .Build();
    
var sender = host.Services.GetRequiredService<IEmailSender>();
await sender.SendEmailAsync("brady@bradykelly.net", "Email Confirmation", "Please confirm your email address.");    