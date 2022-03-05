using Frontegg.SDK.AspNet;
using Frontegg.SDK.AspNet.Proxy;

namespace Centenary.Mvc;

public class MyFronteggProxyInfoExtractor: IFronteggProxyInfoExtractor
{
    public Task<FronteggTenantInfo> Extract(HttpRequest request)
    {
        return Task.FromResult(new FronteggTenantInfo
        {
            UserId = request.Headers["X-Frontegg-User-Id"],
            TenantId = request.Headers["X-Frontegg-Tenant-Id"]
        });
    }
}