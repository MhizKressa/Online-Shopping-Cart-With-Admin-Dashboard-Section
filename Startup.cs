using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KressaFashionHub.Startup))]
namespace KressaFashionHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
