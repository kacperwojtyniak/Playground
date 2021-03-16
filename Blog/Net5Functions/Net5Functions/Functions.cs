using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Net5Functions
{
    public static class Functions
    {
        [FunctionName("Hello")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "hello/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            log.LogInformation("Processed function in .net core");

            string responseMessage = $"Hello {name}. Welcome to .net core 3 functions.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("Cron")]
        public static void Cron([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogTrace("Hello from cron .net 3");
        }
    }
}

