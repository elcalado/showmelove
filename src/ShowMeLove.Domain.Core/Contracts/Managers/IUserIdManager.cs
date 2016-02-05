using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Managers
{
    public interface IUserIdManager
    {
       Task<string> GetAsync();

       Task<bool> InitializeAsync();
    }
}
