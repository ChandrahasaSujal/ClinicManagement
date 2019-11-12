using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CM.Web.App_Start.Startup))]
namespace CM.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
