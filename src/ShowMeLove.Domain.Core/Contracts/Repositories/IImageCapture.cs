using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface IImageCapture
    {
        Task<BitmapImage> CaptureJpegImageAsync();
    }
}
