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
        public async Task<WriteableBitmap> CaptureJpegImageAsync()
        {
            var captureDevice = new MediaCapture();

            await captureDevice.InitializeAsync();

            var imageFormat = ImageEncodingProperties.CreateJpeg();
            var bitmap      = new WriteableBitmap(400,300);

            using (var memoryStream = new InMemoryRandomAccessStream())
            {
                await captureDevice.CapturePhotoToStreamAsync(imageFormat, memoryStream);

                memoryStream.Seek(0);

                await bitmap.SetSourceAsync(memoryStream);
            }

            return bitmap;
        }
    }
}
