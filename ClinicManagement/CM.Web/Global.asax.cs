using CM.Data;
using CM.Data.Infrastructure;
using CM.Data.ViewModels;
using CM.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            AreaRegistration.RegisterAllAreas();
            //Remove All View Engine  
            ViewEngines.Engines.Clear();
            //Add Razor View Engine  
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Seed.Initialize(ApplicationDbContext.Create());
        }
    }
}
