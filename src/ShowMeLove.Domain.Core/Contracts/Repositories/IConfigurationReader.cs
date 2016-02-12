using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface IConfigurationReader
    {
        string this[string settingName] { get; }


        Task InitializeAsync();
    }
}
