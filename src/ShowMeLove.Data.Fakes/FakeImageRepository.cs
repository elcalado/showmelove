using ShowMeLove.Domain.Core.Contracts.Repositories;
using System.Threading.Tasks;

namespace ShowMeLove.Data.Fakes
{
    public class FakeImageRepository : IImageRepository
    {

        public Task<string> SaveAsync(byte[] imageData, string imageName)
        {
            // For a good time, call Luis :)
            return Task.FromResult<string>(null);
        }
    }
}
