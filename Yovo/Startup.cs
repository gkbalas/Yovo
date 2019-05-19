using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Yovo.Startup))]
namespace Yovo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
