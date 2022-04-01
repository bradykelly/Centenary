using System.Text.Json;
using Centenary.Models.DocTree;
using Centenary.Razor.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Centenary.Razor.Pages;

public class DocumentListModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApiOptions _options;

    public DocumentListModel(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public DocumentList DocumentList { get; set; } = new DocumentList();
    
    public Folder ParentFolder { get; set; } = new Folder();
    
    public string ParentName => string.IsNullOrEmpty(ParentFolder.Name) ? "Root" : ParentFolder.Name;
    
    
    public async Task OnGet(string parentPath = "")
    {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_options.BaseUrl}/documents/{parentPath}")
        {
            Headers =
            {
                { HeaderNames.Accept, "application/json" }
            }
        };   
        
        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            DocumentList = await JsonSerializer.DeserializeAsync<DocumentList>(contentStream) ?? new DocumentList();
            ParentFolder = DocumentList.Folders.FirstOrDefault(f => f.FullPath == parentPath) ?? new Folder();
        }
    }
}