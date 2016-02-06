using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmlApi.Startup))]
namespace SmlApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
