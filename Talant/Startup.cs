using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Talant.Startup))]
namespace Talant
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
