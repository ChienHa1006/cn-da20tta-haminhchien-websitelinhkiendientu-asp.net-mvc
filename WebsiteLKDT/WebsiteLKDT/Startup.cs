using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteLKDT.Startup))]
namespace WebsiteLKDT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
