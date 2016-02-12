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
        private AuthenticationResult _authenticationResult;
        private readonly IConfigurationReader _configurationReader;

        public UserIdManager(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }
        
        public async Task<bool> InitializeAsync()
        {
            var tenant      = _configurationReader["AAD_Tenant"];
            var instance    = _configurationReader["AAD_Instance"];
            var resourceId  = _configurationReader["AAD_ResourceId"];
            var clientId    = _configurationReader["AAD_ClientId"];
            var callbackUrl = _configurationReader["AAD_CallbackUri"];

            var callbackUri = new Uri(callbackUrl);
            var authority = String.Format(CultureInfo.InvariantCulture, instance, tenant);

            var redirectUri = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            var authContext = new AuthenticationContext(authority);

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
