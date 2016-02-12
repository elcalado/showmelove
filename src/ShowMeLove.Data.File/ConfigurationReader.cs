using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace ShowMeLove.Data.File
{
    public class ConfigurationReader : IConfigurationReader
    {
        private XDocument _configuration;
        private readonly IExceptionHandler _exceptionHandler;


        public ConfigurationReader(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }


        public string this[string settingName]
        {
            get
            {
                // Get a setting from our XML configuration file
                var first = _configuration.Root.Descendants().FirstOrDefault(e => e.Name.LocalName.Equals("add") && e.Attribute("key").Value.Equals(settingName));
                
                if (first == null)
                    return string.Empty;

                return first.Attribute("value").Value;                                
            }
        }


        public async Task InitializeAsync()
        {
            var configurationFile = await GetConfigurationFileFromFutureAccessListAsync();

            if(configurationFile == null) {
                configurationFile = await OpenConfigurationFileWithDialogAsync();
            }
            _configuration = await Task.Run(() => XDocument.Load(configurationFile.Path));            
        }


        private static async Task<StorageFile> GetConfigurationFileFromFutureAccessListAsync()
        {
            var files = StorageApplicationPermissions.FutureAccessList.Entries;
            if (files.Count > 0)
            {
                var token = files.First().Token;
                return await StorageApplicationPermissions.FutureAccessList.GetFileAsync(token);
            }
            return null;
        }


        private static async Task<StorageFile> OpenConfigurationFileWithDialogAsync()
        {
            var fileOpener = new Windows.Storage.Pickers.FileOpenPicker();
            fileOpener.FileTypeFilter.Add(".config");

            var pickedFile = await fileOpener.PickSingleFileAsync();
            if (pickedFile != null)
            {
                StorageApplicationPermissions.FutureAccessList.Add(pickedFile);
            }
            return pickedFile;
        }
    }
}
