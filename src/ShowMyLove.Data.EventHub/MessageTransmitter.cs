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
        private static string eventHubConnectionstring = "Endpoint=sb://showlovens.servicebus.windows.net/;SharedAccessKeyName=read;SharedAccessKey=uYFJwXmV0d0Ta8kDW03MLadfLAEkpUch0zP1EeqcfFg=";
        private EventHubClient _eventHubClient;

        public MessageTransmitter()
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionstring, "showlove");
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
