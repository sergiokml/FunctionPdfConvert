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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;

namespace FuncPdfConvert
{
    internal class ConvertToPdf
    {
        private readonly Options options;
        public ConvertToPdf(IOptions<Options> options)
        {
            this.options = options.Value;
        }

        [FunctionName("ConvertToPdf")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string tmpFileName = $"{Guid.NewGuid()}{MimeTypes.MimeTypeMap.GetExtension(req.ContentType)}";
                GraphServiceClient client = TokenAuth();

                // UPLOAD FILE
                DriveCollectionResponse drives = await client!.Drives.GetAsync();
                var drive = drives.Value.FirstOrDefault();
                await client.Drives[drive.Id    ].Root.ItemWithPath(tmpFileName)
                      .Content.PutAsync(req.Body, null, CancellationToken.None);
                log.LogInformation("Upload to folder: {a}", drive.Name);
                
                // DOWNLOAD FILE AS PDF
                var requestInformation = client.Drives[drive.Id].Root
                    .ItemWithPath(Path.GetFileName(tmpFileName))
                    .Content.ToGetRequestInformation();
                requestInformation.UrlTemplate += "{?format}"; 
                requestInformation.QueryParameters.Add("format", "pdf");
                var stream = await client.RequestAdapter.SendPrimitiveAsync<Stream>(requestInformation);
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
                    options.TenantId,
                   options.ClientId,
                    options.ClientSecret,
                    new() { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud }
                );
            GraphServiceClient graphClient = new(clientSecretCredential, scopes);
            return graphClient;
        }

    }
}
