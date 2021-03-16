using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;


namespace Net5Functions
{
    public static class Functions
    {
        [Function("Hello")]
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "hello/{name}")] HttpRequestData req,
            string name,
            FunctionContext ctx)
        {
            ctx.GetLogger("hello").LogInformation("Processed function in .net core");

            string responseMessage = $"Hello {name}. Welcome to .net 5 functions.";

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString(responseMessage);
            return response;
        }

        [Function("Cron")]
        public static void Cron([TimerTrigger("0 */1 * * * *")] FunctionContext ctx)
        {
            ctx.GetLogger("cron").LogInformation("Hello from cron .net 5");
        }
    }
}

