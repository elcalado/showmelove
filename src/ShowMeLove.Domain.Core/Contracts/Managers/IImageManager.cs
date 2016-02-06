using ShowMeLove.Domain.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.Domain.Core.Contracts.Managers
{
    public interface IImageManager
    {
        Task<bool> UploadImageAsync();

        Task<bool> InitializeAsync();

        Task<WriteableBitmap> GetBitmapAsync();

        Task<IEnumerable<SentimentResult>> GetSentimentsAsync(WriteableBitmap bitmap);
    }
}
