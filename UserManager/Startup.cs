using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserManager.Startup))]
namespace UserManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
