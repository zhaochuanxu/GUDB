using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GUDB.UI.Startup))]
namespace GUDB.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
