using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace Functions.ImageAnalyzer
{
    public static class GetSasTokenFunctions
    {
        [FunctionName(nameof(GetSasToken))]
        public static IActionResult GetSasToken(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetSasToken/{name}")] HttpRequest req,
            [Blob("%BlobContainerPath%/{name}", FileAccess.ReadWrite, Connection = "BlobConnection")] CloudBlockBlob blob)
        {
            var policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Create,
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(10)
            };
            var sas = blob.GetSharedAccessSignature(policy);
            var result = blob.Uri + sas;
            return new OkObjectResult(result);
        }
    }
}
