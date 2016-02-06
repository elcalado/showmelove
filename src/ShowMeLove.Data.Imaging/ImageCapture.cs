using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;

namespace ShowMeLove.Data.Imaging
{
    public class ImageCapture : IImageCapture
    {
        public async Task<InMemoryRandomAccessStream> CaptureJpegImageAsync()
        {
            var captureDevice = new MediaCapture();
            await captureDevice.InitializeAsync();

            var imageFormat = ImageEncodingProperties.CreateJpeg();
            var memoryStream = new InMemoryRandomAccessStream();
            await captureDevice.CapturePhotoToStreamAsync(imageFormat, memoryStream);

            return memoryStream;
        }
    }
}
