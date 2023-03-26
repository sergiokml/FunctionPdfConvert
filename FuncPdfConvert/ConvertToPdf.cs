using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Azure.Identity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using Microsoft.Kiota.Abstractions;

namespace FuncPdfConvert
{
    internal class ConvertToPdf
    {
        private readonly IConfiguration configuration;

        public ConvertToPdf(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [FunctionName("ConvertToPdf")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
                HttpRequest req,
            ILogger log
        )
        {
            try
            {
                string tmpFileName =
                    $"{Guid.NewGuid()}{MimeTypes.MimeTypeMap.GetExtension(req.ContentType)}";
                GraphServiceClient client = TokenAuth();

                // UPLOAD FILE
                DriveCollectionResponse drives = await client!.Drives.GetAsync();
                Drive drive = drives.Value.FirstOrDefault();
                await client.Drives[drive.Id].Root
                    .ItemWithPath(tmpFileName)
                    .Content.PutAsync(req.Body, null, CancellationToken.None);
                log.LogInformation("Upload to folder: {a}", drive.Name);

                // DOWNLOAD FILE AS PDF
                RequestInformation requestInformation = client.Drives[drive.Id].Root
                    .ItemWithPath(Path.GetFileName(tmpFileName))
                    .Content.ToGetRequestInformation();
                requestInformation.UrlTemplate += "{?format}";
                requestInformation.QueryParameters.Add("format", "pdf");
                Stream stream = await client.RequestAdapter.SendPrimitiveAsync<Stream>(
                    requestInformation
                );
                return new OkObjectResult(stream);
            }
            catch (ODataError odataError)
            {
                Console.WriteLine(odataError.Error.Code);
                Console.WriteLine(odataError.Error.Message);
                throw;
            }
        }

        public GraphServiceClient TokenAuth()
        {
            string[] scopes = { "https://graph.microsoft.com/.default" };
            ClientSecretCredential clientSecretCredential =
                new(
                    configuration.GetValue<string>("ADConfig:TenantId"),
                    configuration.GetValue<string>("ADConfig:ClientId"),
                    configuration.GetValue<string>("ADConfig:ClientSecret"),
                    new() { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud }
                );
            GraphServiceClient graphClient = new(clientSecretCredential, scopes);
            return graphClient;
        }
    }
}
