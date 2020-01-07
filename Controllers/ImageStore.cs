using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage.DataMovement;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;


namespace AzureWebApp_SQL_Service.Controllers
{
    public class ImageStore : IImageStore
    {
        private CloudBlobClient blobClient;
        private string baseUri = "https://azurehellostorage.blob.core.windows.net";
        public ImageStore()
        {
            var key = "EIu7Fp/25sO0ZQtiu/RWggQjXvUDtV4GSvuY2OiqL2HtZFf2uE/OL/5uvk+YVSNggOSNdzd6frTbWZVEqnb8pA==";
            var storageAccountName = "azurehellostorage";

            var creds = new StorageCredentials(storageAccountName, key);
            this.blobClient = new CloudBlobClient(new Uri(baseUri), creds);
        }

        [RequestSizeLimit(400000)]
      
        public async Task<string> SaveImage(Stream stream, string fileName)
        {
            var id = Guid.NewGuid().ToString();
            
            var container = this.blobClient.GetContainerReference("images");
          
            var blob = container.GetBlockBlobReference(id);
            var options = new BlobRequestOptions();
            options.ServerTimeout = new TimeSpan(1, 1, 1);
            options.MaximumExecutionTime = new TimeSpan(1, 1, 1);
            options.ParallelOperationThreadCount = 64;
            try
            {
                await blob.UploadFromFileAsync(fileName, null, options, null);
            }
            catch (Exception ex)
            {

                throw;
            }
           
            //await blob.UploadFromStreamAsync(stream);
            return id;
        }

        public Uri UriFor(string imageId)
        {
            return new Uri(this.baseUri + "images/" + imageId);
        }
    }
}