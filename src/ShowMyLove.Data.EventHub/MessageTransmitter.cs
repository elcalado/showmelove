using Newtonsoft.Json;
using ppatierno.AzureSBLite.Messaging;
using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.Domain.Core.Entities;
using System.Text;
using System.Threading.Tasks;

namespace ShowMyLove.Data.EventHub
{
    public class MessageTransmitter : IMessageTransmitter
    {
        private readonly IConfigurationReader _configurationReader;
        private readonly IExceptionHandler _exceptionHandler;

        private EventHubClient _eventHubClient;


        public MessageTransmitter(IExceptionHandler exceptionHandler, IConfigurationReader configurationReader)
        {
            _exceptionHandler = exceptionHandler;
            _configurationReader = configurationReader;
        }


        public async Task InitializeAsync()
        {
            var connectionString = _configurationReader["EventHubConnectionString"];
            var eventHubPath     = _configurationReader["EventHubPath"];

            _eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubPath);

            await Task.FromResult<object>(null);
        }


        public async Task TransmitImageSavedAsync(SentimentResult result)
        {
            var serialized = JsonConvert.SerializeObject(result);
            var eventData = new EventData(Encoding.UTF8.GetBytes(serialized));

            _exceptionHandler.Run(() =>  _eventHubClient.Send(eventData));

            await Task.FromResult<object>(null);
        }
    }
}
