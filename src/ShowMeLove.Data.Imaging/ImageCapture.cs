using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.Data.Imaging
{
    public class ImageCapture : IImageCapture
    {
        public async Task<WriteableBitmap> CaptureJpegImageAsync()
        {
            var captureDevice = new MediaCapture();

            await captureDevice.InitializeAsync();

            var imageFormat = ImageEncodingProperties.CreateJpeg();
            var bitmap      = new WriteableBitmap(400,300);

            using (var memoryStream = new InMemoryRandomAccessStream())
            {
                var file = await Windows.Storage.KnownFolders.PicturesLibrary.CreateFileAsync("lastImageCapture.jpg", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                var imageEncodingProperties = ImageEncodingProperties.CreateJpeg();
                await captureDevice.CapturePhotoToStorageFileAsync(imageEncodingProperties, file); //  CapturePhotoToStreamAsync(imageFormat, memoryStream);

                var photoStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                await bitmap.SetSourceAsync(photoStream);
            }

            return bitmap;
        }


    }
}
