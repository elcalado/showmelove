using Microsoft.IdentityModel.Clients.ActiveDirectory;
using ShowMeLove.Domain.Core.Contracts.Managers;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ShowMeLove.Data.Identity
{
    public class UserIdManager : IUserIdManager
    {
        static string tenant             = "showmelove.onmicrosoft.com"; 
        static string clientId           = "[Enter client ID as obtained from Azure Portal, e.g. 82692da5-a86f-44c9-9d53-2f88d52b478b]"; 
        static string aadInstance        = "https://login.microsoftonline.com/{0}";
        static string authority          = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
        static string someShitResourceId = "";


        private AuthenticationResult _authenticationResult;

        public UserIdManager()
        {

        }

        public async Task<bool> InitializeAsync()
        {
            var redirectUri = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            var authContext = new AuthenticationContext(authority);
            Uri noUri = null;

            _authenticationResult = await authContext.AcquireTokenAsync(someShitResourceId, clientId, noUri, null);

            if (_authenticationResult == null)
                return false;

            return true;

        }

        

        public Task<string> GetAsync()
        {
            return Task.FromResult<string>(_authenticationResult.UserInfo.UniqueId);
        }
    }
}
