using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Notes.Api.Infrastructure.EntityFramework;
using Owin;
using tc_dev.Core.Infrastructure.Identity;

namespace Notes.Api.Bootstrapper
{
    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }  

        public void ConfigureAuth(IAppBuilder app) {

            DataProtectionProvider = app.GetDataProtectionProvider();

            app.UseOAuthBearerTokens(CreateOAuthOptions());
        private OAuthAuthorizationServerOptions CreateOAuthOptions() {
            return new OAuthAuthorizationServerOptions {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new AppOAuthProvider(
                    "self",
                    () => IdentityFactory.CreateUserManager(new NotesDbContext())),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true

            app.UseOAuthBearerTokens(oAuthOptions);
        }
    }
}
