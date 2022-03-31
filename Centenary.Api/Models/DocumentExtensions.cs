using Centenary.Api.Data.Models;
using Centenary.Models.DocTree;

namespace Centenary.Api.Models;

public static class DocumentExtensions
{
    public static DocumentDto ToDto(this Document document)
    {
        return new DocumentDto
        {
            FullPath = document.FullPath,
            Description = document.Description,
            CreatedBy = document.CreatedBy,
            CreatedOn = document.CreatedOn
        };        
    }
}