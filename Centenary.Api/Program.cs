using Centenary.Api.Data;
using Centenary.Api.Models;
using Centenary.Api.Service;
using Centenary.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "app.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"DataSource={dbPath};Cache=Shared"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<BlobOptions>(builder.Configuration.GetSection("Azure:Blobs"));
builder.Services.AddScoped<IBlobApiClient, BlobApiClient>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();