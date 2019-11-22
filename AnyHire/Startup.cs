using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnyHire.Startup))]
namespace AnyHire
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
