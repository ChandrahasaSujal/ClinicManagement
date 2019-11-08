using CM.Web.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CM.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Init Database
            //System.Data.Entity.Database.SetInitializer(null);

            //Remove All View Engine  
            ViewEngines.Engines.Clear();
            //Add Razor View Engine  
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Autofac and Automapper configurations
            Bootstrapper.Run();
        }
    }
}
