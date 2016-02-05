using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface IMessageTransmitter
    {
        Task TransmitImageSavedAsync(string userId, string blobName);
    }
}
