using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Functions.ImageAnalyzer
{
    public static class SignalR
    {

        [FunctionName(nameof(negotiate))]
        public static SignalRConnectionInfo negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "imageAnalysis")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}
