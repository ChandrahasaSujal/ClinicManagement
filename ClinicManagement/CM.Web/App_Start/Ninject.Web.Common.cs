[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CM.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CM.Web.App_Start.NinjectWebCommon), "Stop")]

namespace CM.Web.App_Start
{
    using System;
    using System.Web;
    using AutoMapper;
    using CM.Data.Infrastructure;
    using CM.Data.Mappings;
    using CM.Service.ServiceInterfaces;
    using CM.Service.Services;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
                kernel.Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
                var mapper = kernel.Get<IMapper>();

                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
                kernel.Bind<IAppointmentService>().To<AppointmentService>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}