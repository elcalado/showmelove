using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface IImageCapture
    {
        Task<InMemoryRandomAccessStream> CaptureJpegImageAsync();
    }
}
