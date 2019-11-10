using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CM.Web.Startup))]
namespace CM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
