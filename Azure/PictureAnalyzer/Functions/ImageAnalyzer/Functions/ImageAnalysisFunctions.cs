using ImageAnalyzer.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Functions.ImageAnalyzer
{
    public static class AnalyzeImageFromBlobFunction
    {
        // Specify the features to return
        private static readonly List<VisualFeatureTypes> features =
            new List<VisualFeatureTypes>()
        {
            VisualFeatureTypes.Tags
        };
        private static ComputerVisionClient computerVisionClient;

        [FunctionName(nameof(AnalyzeImageFromBlob))]
        [return: ServiceBus("imageanalyzedtopic", Connection = "ServiceBusConnection")]
        public static async Task<AnalysisResult> AnalyzeImageFromBlob(
            [BlobTrigger("%BlobContainerPath%", Connection = "BlobConnection")]CloudBlockBlob imageBlob)
        {
            var blobAccessPolicy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5)
            };

            var sas = imageBlob.GetSharedAccessSignature(blobAccessPolicy);
            var imageUrl = imageBlob.Uri + sas;

            var computerVisionClient = GetComputerVisionClient();

            var computerVisionResult = await computerVisionClient.AnalyzeImageAsync(imageUrl, features);
            var tags = JsonConvert.SerializeObject(computerVisionResult.Tags.Select(t => t.Name));

            return new AnalysisResult("Analyzed", imageBlob.Name, tags, new Uri(imageUrl));
        }

        [FunctionName(nameof(ProcessAnalysisResult))]
        [return: Table("AnalysisResults", Connection = "BlobConnection")]
        public static async Task<AnalysisResult> ProcessAnalysisResult(
            [ServiceBusTrigger("imageanalyzedtopic", "S1", Connection = "ServiceBusConnection")] AnalysisResult imageBlob,
            [SignalR(HubName = "imageAnalysis")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            await AddPictureToSignalR();
            return imageBlob;

            async Task AddPictureToSignalR()
            {
                var msg = new
                {
                    tags = imageBlob.Tags,
                    url = imageBlob.ImageUrl.ToString()
                };
                await signalRMessages.AddAsync(new SignalRMessage()
                {
                    Target = "analysisReceived",
                    Arguments = new object[] { msg }
                });
            }
        }
        private static ComputerVisionClient GetComputerVisionClient()
        {
            if (computerVisionClient == null)
            {
                var computerVisionKey = System.Environment.GetEnvironmentVariable("COMPUTER_VISION_KEY");
                var computerVisionUrl = System.Environment.GetEnvironmentVariable("COMPUTER_VISION_URL");

                computerVisionClient = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(computerVisionKey),
                new System.Net.Http.DelegatingHandler[] { });

                computerVisionClient.Endpoint = computerVisionUrl;
            }

            return computerVisionClient;
        }

    }
}
