using System;
using System.Threading.Tasks;
using IntelligentBusinessApi.Configuration;
using IntelligentBusinessApi.Contracts;
using IntelligentBusinessApi.DataTransferObjects;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace IntelligentBusinessApi.Services
{
    public sealed class ValetKeyService : IValetKeyService
    {

        private CloudStorageAccount storageAccount;
        private CloudBlobClient cloudBlobClient;
        private CloudBlobContainer cloudContainer;
        private const string importContainer = "valet-key-uploads";

        public ValetKeyService(IOptionsMonitor<CloudConfiguration> blobConfig)
        {
            string storageConnectionString = blobConfig.CurrentValue.StorageConnectionString;
            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount account))
            {
                storageAccount = account;
                cloudBlobClient = storageAccount.CreateCloudBlobClient();
                cloudContainer = cloudBlobClient.GetContainerReference(importContainer);
            }
        }

        public async Task<ValetKeyDto> RegisterValetKey(string blobName)
        {
            await cloudContainer.CreateIfNotExistsAsync();

            var blob = cloudContainer.GetBlockBlobReference(blobName);
            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Write,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(20)
            };
            var sas = blob.GetSharedAccessSignature(policy);
            return new ValetKeyDto
            {
                StorageUri = $"https://{blob.Container.Uri.Host}",
                StorageAccessToken= sas,
                Container= blob.Container.Name,
                Filename= blob.Name
        };
        }

    }
}
