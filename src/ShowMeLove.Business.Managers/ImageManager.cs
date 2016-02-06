using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using System.Linq;

namespace ShowMeLove.Business.Managers
{
    public class ImageManager : IImageManager
    {
        private readonly IUserIdManager _userIdManager;
        private readonly IImageCapture _imageCapture;
        private readonly IImageRepository _imageRepository;
        private readonly IMessageTransmitter _messageTransmitter;
        private readonly IOxfordClient _oxfordClient;

        public ImageManager(IUserIdManager userIdManager, IImageRepository imageRepository,
            IImageCapture imageCapture, IMessageTransmitter messageTransmitter,
            IOxfordClient oxfordClient)
        {
            _userIdManager = userIdManager;
            _imageRepository = imageRepository;
            _imageCapture = imageCapture;
            _messageTransmitter = messageTransmitter;
            _oxfordClient = oxfordClient;
        }


        public async Task<bool> UploadImageAsync()
        {
            return true;
        }


        public async Task<bool> InitializeAsync()
        {
            var idManagerOk = await _userIdManager.InitializeAsync();

            if (!idManagerOk) return false;

            return true;
        }

        public async Task<WriteableBitmap> GetBitmapAsync()
        {
            return await _imageCapture.CaptureJpegImageAsync();
        }


        public async Task<IEnumerable<SentimentResult>> GetSentimentsAsync(WriteableBitmap bitmap)
        {
            var file = await Windows.Storage.KnownFolders.PicturesLibrary.CreateFileAsync("lastImageCapture.jpg", Windows.Storage.CreationCollisionOption.OpenIfExists);

            if (file == null)
                return null;

            var fileStream = await file.OpenStreamForReadAsync();
            return await _oxfordClient.GetSentimentsFromImageAsync(fileStream);
        }

        public async Task TransmitSentimentsAsync(IEnumerable<SentimentResult> sentiments)
        {
            if (sentiments == null || !sentiments.Any())
                return;

            foreach (var sentiment in sentiments)
                await _messageTransmitter.TransmitImageSavedAsync(sentiment);
        }
    }
}
