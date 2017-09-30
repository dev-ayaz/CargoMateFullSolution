using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CargoMate.Web.Admin.Startup))]
namespace CargoMate.Web.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
