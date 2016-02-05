using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Repositories

{
    public interface IImageRepository
    {
        Task<string> SaveAsync(byte[] imageData, string imageName);
    }
}
