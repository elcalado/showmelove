using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.Data.Imaging
{
    public class ImageCapture : IImageCapture
    {
        private const string IMAGECAPTURE_FILENAME = "LastImageCapture.jpg";
        private MediaCapture _mediaCapture;
        private ImageEncodingProperties _imageEncodingProperties;

        private readonly IExceptionHandler _exceptionHandler;

        public ImageCapture(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public async Task InitializeAsync()
        {
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();

            _imageEncodingProperties = ImageEncodingProperties.CreateJpeg();
        }



        public async Task<WriteableBitmap> CaptureJpegImageAsync()
        {
            var bitmap      = new WriteableBitmap(400,300);

            using (var memoryStream = new InMemoryRandomAccessStream())
            {
                var file = await Windows.Storage.KnownFolders.PicturesLibrary.CreateFileAsync(IMAGECAPTURE_FILENAME, Windows.Storage.CreationCollisionOption.ReplaceExisting);

                await _exceptionHandler.Run(async () =>
                {
                    await _mediaCapture.CapturePhotoToStorageFileAsync(_imageEncodingProperties, file);
                    var photoStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    await bitmap.SetSourceAsync(photoStream);
                });
            }
            return bitmap;
        }
    }
}
