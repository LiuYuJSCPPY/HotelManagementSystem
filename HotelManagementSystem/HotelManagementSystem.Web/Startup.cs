using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelManagementSystem.Web.Startup))]
namespace HotelManagementSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
