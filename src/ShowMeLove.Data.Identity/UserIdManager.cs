using Microsoft.IdentityModel.Clients.ActiveDirectory;
using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ShowMeLove.Data.Identity
{
    public class UserIdManager : IUserIdManager
    {
        static string tenant      = "showmelove.onmicrosoft.com";
        static string clientId    = "b9852d74-58b3-440b-a097-548dfba43f74 ";
        static string aadInstance = "https://login.microsoftonline.com/{0}";
        static string resourceId  = "https://graph.windows.net/";
        static string authority   = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        private AuthenticationResult _authenticationResult;
        private readonly IConfigurationReader _configurationReader;

        public UserIdManager(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }
        
        public async Task<bool> InitializeAsync()
        {
            var redirectUri = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            var authContext = new AuthenticationContext(authority);

            var callBackUri = _configurationReader["AuthenticationCallbackUri"];

            Uri callbackUri = new Uri("http://showmylove.azurewebsites.net");

            var platformParameters = new PlatformParameters(PromptBehavior.Auto, false);

            _authenticationResult = await authContext.AcquireTokenAsync(resourceId, clientId, callbackUri, platformParameters);

            if (_authenticationResult == null)
                return false;

            return true;

        }


        public Task<string> GetAsync()
        {
            if (_authenticationResult == null)
                return Task.FromResult(string.Empty);

            return Task.FromResult(_authenticationResult.UserInfo.UniqueId);
        }
    }
}
