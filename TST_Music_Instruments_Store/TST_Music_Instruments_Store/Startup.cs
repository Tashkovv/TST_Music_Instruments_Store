using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TST_Music_Instruments_Store.Startup))]
namespace TST_Music_Instruments_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
