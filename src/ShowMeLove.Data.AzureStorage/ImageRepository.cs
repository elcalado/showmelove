using System;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace ShowMeLove.Data.AzureStorage
{
    public class ImageRepository : IImageRepository
    {
        private readonly CloudBlobClient _blobClient;
        private CloudBlobContainer _containerReference;

        public ImageRepository()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=showmelove;AccountKey=a6OrMwyImPsWdg6eoJvspaM1foqYtE4sPZIr4dpWk2BoFrfhANqYGOz6jQf/UYzfHZE9djfqMdXzCDBLA05bZQ==;";
            var cloudStorageaccount = CloudStorageAccount.Parse(connectionString);
            _blobClient = cloudStorageaccount.CreateCloudBlobClient();
        }


        public async Task InitializeAsync()
        {
            _containerReference = _blobClient.GetContainerReference("images");
            try
            {
                await _containerReference.CreateIfNotExistsAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> SaveAsync(byte[] imageData, string imageName)
        {
            var finalBlobName = _containerReference.GetBlockBlobReference(imageName);

            await finalBlobName.UploadFromByteArrayAsync(imageData, 0, imageData.Length);

            return finalBlobName.Uri.ToString();
        }
    }
}