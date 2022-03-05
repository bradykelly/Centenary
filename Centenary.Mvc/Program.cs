using Centenary.Mvc;
using Frontegg.SDK.AspNet;
using Frontegg.SDK.AspNet.Proxy;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.TryAddSingleton<IFronteggProxyInfoExtractor, MyFronteggProxyInfoExtractor>();
builder.Services.AddFrontegg(options =>
{
    options.ApiKey = builder.Configuration["Frontegg:ApiKey"];
    options.ClientId = builder.Configuration["Frontegg:ClientId"];
    options.ThrowOnMissingConfiguration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseFrontegg<MyFronteggProxyInfoExtractor>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();