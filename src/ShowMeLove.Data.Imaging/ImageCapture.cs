using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.Data.Imaging
{
    public class ImageCapture : IImageCapture
    {
        public async Task<BitmapImage> CaptureJpegImageAsync()
        {
            var captureDevice = new MediaCapture();

            await captureDevice.InitializeAsync();
            var bitmap = new BitmapImage();
            var imageFormat = ImageEncodingProperties.CreateJpeg();

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
