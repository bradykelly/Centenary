using System.Threading.Tasks;
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Centenary.Functions;

public static class SendMail
{
    [FunctionName("SendMail")]
    public static async Task RunAsync(
        [QueueTrigger("email", Connection = "Azure:Storage:ConnectionString")] string myQueueItem, ILogger log)
    {
        log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        
    }
}