using Centenary.Mvc.Data;
using Centenary.Storage;

namespace Centenary.Mvc.Service;

public class DocumentTreeService
{
    private readonly IBlobApiClient _blobApiClient;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DocumentTreeService> _logger;

    public DocumentTreeService(IBlobApiClient blobApiClient, ApplicationDbContext dbContext, ILogger<DocumentTreeService> logger)
    {
        _blobApiClient = blobApiClient;
        _dbContext = dbContext;
        _logger = logger;
    }
}