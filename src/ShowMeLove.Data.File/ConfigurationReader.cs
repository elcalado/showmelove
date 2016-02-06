using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
