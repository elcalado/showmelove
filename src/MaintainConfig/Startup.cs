using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaintainConfig.Startup))]
namespace MaintainConfig
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
