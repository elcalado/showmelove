using Newtonsoft.Json;
using ppatierno.AzureSBLite.Messaging;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.Domain.Core.Entities;
using System.Text;
using System.Threading.Tasks;

namespace ShowMyLove.Data.EventHub
{
    public class MessageTransmitter : IMessageTransmitter
    {
        private static string eventHubConnectionstring = "Endpoint=sb://showmelove.servicebus.windows.net/;SharedAccessKeyName=policysml;SharedAccessKey=kJd1wMpwjpWolfkCs3TsgOD/IhyZnVXptQld1MGHUcs=";
        private EventHubClient _eventHubClient;

        public MessageTransmitter()
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionstring, "eventhub");
        }


        public async Task TransmitImageSavedAsync(SentimentResult result)
        {
            var serialized = JsonConvert.SerializeObject(result);
            var eventData = new EventData(Encoding.UTF8.GetBytes(serialized));

            try
            {
                await Task.Run(() => _eventHubClient.Send(eventData));
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
