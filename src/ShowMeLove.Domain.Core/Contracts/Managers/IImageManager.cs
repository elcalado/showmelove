using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Managers
{
    public interface IImageManager
    {
        Task<bool> UploadImageAsync();
    }
}
