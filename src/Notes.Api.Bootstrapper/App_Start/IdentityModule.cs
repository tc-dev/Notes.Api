using System.Data.Entity;
using System.Web;
using Autofac;
using Microsoft.AspNet.Identity.Owin;
using tc_dev.Core.Identity;
using tc_dev.Core.Infrastructure.EntityFramework;
using tc_dev.Core.Infrastructure.Identity;
using tc_dev.Core.Infrastructure.Identity.Models;

namespace Notes.Api.Bootstrapper
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder) {

            builder.RegisterType(typeof (AppUserManager)).As(typeof (IAppUserManager))
                .InstancePerRequest();

            builder.RegisterType(typeof (AppRoleManager)).As(typeof (IAppRoleManager))
                .InstancePerRequest();

            builder.RegisterType(typeof (AppAuthenticationManager)).As(typeof (IAppAuthenticationManager))
                .InstancePerRequest();

            builder.Register(b => b.Resolve<IDbContext>() as DbContext)
                .InstancePerRequest();

            builder.Register(b => {
                var manager = IdentityFactory.CreateUserManager(b.Resolve<DbContext>());

                if (Startup.DataProtectionProvider != null) {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<AppIdentityUser, int>(
                            Startup.DataProtectionProvider.Create("ASP.NET Identity"));
                }

                return manager;
            }).InstancePerRequest();

            builder.Register(b => IdentityFactory.CreateRoleManager(b.Resolve<DbContext>()))
                .InstancePerRequest();

            builder.Register(b => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();

        }
    }
}
