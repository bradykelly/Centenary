using Microsoft.EntityFrameworkCore;
using Centenary.Storage;
using Centenary.Web.Data;
using Centenary.Web.Models;
using Centenary.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbPath = Path.Combine(builder.Environment.WebRootPath, "db", "app.db");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"DataSource={dbPath};Cache=Shared"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();

builder.Services.Configure<BlobOptions>(builder.Configuration.GetSection("Azure:Blobs"));
builder.Services.AddScoped<IBlobApiClient, BlobApiClient>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();