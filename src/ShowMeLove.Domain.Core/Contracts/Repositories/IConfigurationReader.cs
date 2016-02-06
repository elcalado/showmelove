using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface IConfigurationReader
    {
        string this[string settingName] { get; }
    }
}
