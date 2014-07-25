using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace Notes.Api.Bootstrapper
{
    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }  

        public void ConfigureAuth(IAppBuilder app) {

            DataProtectionProvider = app.GetDataProtectionProvider();

        }
    }
}
