using Autofac;
using System.Linq;
using Autofac.Integration.Mvc;
using CM.Data.Mappings;
using System.Reflection;
using CM.Data.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using CM.Data;


namespace CM.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            // Init Database
            //System.Data.Entity.Database.SetInitializer(null);
            //AreaRegistration.RegisterAllAreas();
            //Remove All View Engine  
            ViewEngines.Engines.Clear();
            //Add Razor View Engine  
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Seed.Initialize(ApplicationDbContext.Create());
            // Autofac and Automapper configurations
            //SetAutofacContainer();
            //Configure AutoMapper
            //AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            // Repositories
            //builder.RegisterAssemblyTypes(typeof(GadgetRepository).Assembly)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerRequest();
            // Services
            //builder.RegisterAssemblyTypes(typeof(GadgetService).Assembly)
            //   .Where(t => t.Name.EndsWith("Service"))
            //   .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}