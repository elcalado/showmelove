using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;

namespace ShowMeLove.Data.File
{
    public class ConfigurationReader : IConfigurationReader
    {
        public ConfigurationReader()
        {
        }

        public string this[string settingName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
