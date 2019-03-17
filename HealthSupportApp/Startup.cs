using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthSupportApp.Startup))]
namespace HealthSupportApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
