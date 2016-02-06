using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.Domain.Core.Contracts.Managers
{
    public interface IImageManager
    {
        Task<bool> UploadImageAsync();

        Task<bool> InitializeAsync();

        Task<WriteableBitmap> GetBitmapAsync();
    }
}
