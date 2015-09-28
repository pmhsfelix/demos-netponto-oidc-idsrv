using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp1.Startup))]
namespace WebApp1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
