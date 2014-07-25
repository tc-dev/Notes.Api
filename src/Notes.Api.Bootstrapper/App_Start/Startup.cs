using Owin;

namespace Notes.Api.Bootstrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
