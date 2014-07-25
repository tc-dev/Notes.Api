using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Notes.Api.Bootstrapper;
using Notes.Api.Core.Data;
using Notes.Api.Infrastructure.EntityFramework;
using tc_dev.Core.Infrastructure.EntityFramework;
using tc_dev.Core.Infrastructure.Logging;
using tc_dev.Core.Service;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), "RegisterDependencies")]

namespace Notes.Api.Bootstrapper
{
    public class IocConfig
    {
        public static void RegisterDependencies() {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof (WebApiApplication).Assembly);

            builder.RegisterType(typeof (NotesUnitOfWork)).As(typeof (INotesUnitOfWork))
                .InstancePerRequest();

            builder.Register<IDbContext>(b => {
                var logger = b.Resolve<ILogger>();
                var dbContext = new NotesDbContext("name=AppContext", logger);

                return dbContext;
            }).InstancePerRequest();

            builder.Register(b => NLogLogger.Instance)
                .SingleInstance();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}
