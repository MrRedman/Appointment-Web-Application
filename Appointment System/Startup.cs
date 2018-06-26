using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Appointment_System.Startup))]
namespace Appointment_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
