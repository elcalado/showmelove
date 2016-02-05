using ShowMeLove.Domain.Core.Contracts.Repositories;
using System.Threading.Tasks;

namespace ShowMyLove.Data.EventHub
{
    public class MessageTransmitter : IMessageTransmitter
    {
        public async Task TransmitImageSavedAsync(string userId, string blobName)
        {
            await Task.FromResult<object>(null);
        }
    }
}
