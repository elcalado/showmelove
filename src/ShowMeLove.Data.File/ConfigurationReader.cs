using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using System.Threading.Tasks;
using Windows.Storage;

namespace ShowMeLove.Data.File
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly IExceptionHandler _exceptionHandler;
        private ApplicationDataContainer _localSettings;


        public ConfigurationReader(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }


        public string this[string settingName]
        {
            get
            {
                return _exceptionHandler.Run(() => _localSettings.Values[settingName].ToString(); );
            }
        }


        public async Task InitializeAsync()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            await Task.FromResult<object>(null);
        }
    }
}
