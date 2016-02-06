using System;
using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.Domain.Core.Entities;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;

namespace ShowMeLove.Business.Managers
{
    public class ImageManager : IImageManager
    {
        private readonly IUserIdManager _userIdManager;
        private readonly IImageCapture _imageCapture;
        private readonly IImageRepository _imageRepository;
        private readonly IMessageTransmitter _messageTransmitter;


        public ImageManager(IUserIdManager userIdManager, IImageRepository imageRepository, IImageCapture imageCapture, IMessageTransmitter messageTransmitter)
        {
            _userIdManager = userIdManager;
            _imageRepository = imageRepository;
            _imageCapture = imageCapture;
            _messageTransmitter = messageTransmitter;
        }


        public async Task<bool> UploadImageAsync()
        {
            // Grab ID of the user
            var userId = await _userIdManager.GetAsync();
            if (userId == null)
                return false;

            // Create BLOB name
            var blobName = userId.ToString() + Guid.NewGuid().ToString();

            // Upload the image

            //var imageUploadNotification = new ImageUploadNotification
            //{
            //    UserId = userId,
            //    BlobUrl = blobUrl
            //};

            //// When the image is uploaded, send a message on the event hub with the ID and blob name
            //await _messageTransmitter.TransmitImageSavedAsync(userId, blobName);

            return true;
        }


        public async Task<bool> InitializeAsync()
        {
            var idManagerOk = await _userIdManager.InitializeAsync();

            if (!idManagerOk) return false;

            return true;
        }

        public async Task<BitmapImage> GetBitmapAsync()
        {
            return await GetImageFromWebCam();
        }


        private async Task<BitmapImage> GetImageFromWebCam()
        {
            return await _imageCapture.CaptureJpegImageAsync();
        }

    }
}
