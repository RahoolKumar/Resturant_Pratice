using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interface;
using Restaurants.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Storage
{
    public class BlobStorageService(IOptions<BlobStorageSettings> options) : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings = options.Value;
        public async Task<string> UploadToBlobAsync(Stream data, string fileName)
        {
           var blobSerivceClient =  new BlobServiceClient(_blobStorageSettings.ConnectionString);
            var containerClinet =  blobSerivceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);


            var blobClient = containerClinet.GetBlobClient(fileName);

            await blobClient.UploadAsync(data);
            var blobURL = blobClient.Uri.ToString();
            return blobURL;
        
        }
    }
}
