using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace BackEnd.Movie.AzureBlob
{
    /// <summary>
    /// This class represents the functionalities for 
    /// 1. querying a list of blobs in an azure blob container.
    /// 2. Upload an azure blob to a container.
    /// 3. Download an azure blob content as a Text.
    /// </summary>
    public class AzureBlobStorage : IAzureBlobStorage
    {
        AzureBlobSetings settings;
        public AzureBlobStorage(AzureBlobSetings settings)
        {
            this.settings = settings;
        }

        public async Task UploadTextAsync(string blobName, string content)
        {
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);
            await blockBlob.UploadTextAsync(content);
        }

        public async Task<string> DownloadAsTextAsync(string blobName)
        {
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);
            return await blockBlob.DownloadTextAsync();
        }

        private async Task<CloudBlobContainer> GetContainerAsync()
        {
            // Azure storage emulator will be using in the local environment.
            CloudStorageAccount storageAccount = string.IsNullOrEmpty(settings.StorageAccount) || string.IsNullOrEmpty(settings.StorageKey)
                ? CloudStorageAccount.Parse(settings.DevelopmentConnectionString ?? "UseDevelopmentStorage=true")
                : new CloudStorageAccount(new StorageCredentials(settings.StorageAccount, settings.StorageKey), false);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(settings.ContainerName);

            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });

            return blobContainer;
        }

        private async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
        {
            CloudBlobContainer blobContainer = await GetContainerAsync();
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);
            return blockBlob;
        }
    }
}
