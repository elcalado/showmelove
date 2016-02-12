using ShowMeLove.Domain.Core.Entities;
using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface IMessageTransmitter
    {
        Task TransmitImageSavedAsync(SentimentResult result);

        Task InitializeAsync();
    }
}
