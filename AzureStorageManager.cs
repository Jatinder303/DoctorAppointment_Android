using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace DoctorAppointment_Android
{
    public class AzureStorageManager
    {
        private readonly string _storageConnectionString;

        public AzureStorageManager(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public async Task UploadDatabaseToAzureStorageAsync(string localDatabaseFilePath, string destinationBlobName)
        {
            var blobServiceClient = new BlobServiceClient(_storageConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient("your-container-name");
            var blobClient = containerClient.GetBlobClient(destinationBlobName);

            using (var fileStream = File.OpenRead(localDatabaseFilePath))
            {
                await blobClient.UploadAsync(fileStream, true);
            }
        }
    }
}
