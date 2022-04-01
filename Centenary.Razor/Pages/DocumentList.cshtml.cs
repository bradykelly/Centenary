using System.Text.Json;
using Centenary.Models.DocTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace Centenary.Razor.Pages;

public class DocumentListModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DocumentListModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public DocumentList DocumentList { get; set; } = new DocumentList();
    
    public Folder ParentFolder { get; set; } = new Folder();
    
    public string ParentName => string.IsNullOrEmpty(ParentFolder.Name) ? "Root" : ParentFolder.Name;
    
    
    public async Task OnGet(string parentPath = "")
    {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"{ApiStrings.BaseUrl}/documents/{parentPath}")
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