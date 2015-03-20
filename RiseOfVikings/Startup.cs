using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RiseOfVikings.Startup))]
namespace RiseOfVikings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
