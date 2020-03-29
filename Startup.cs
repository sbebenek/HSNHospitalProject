using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HSNHospitalProject.Startup))]
namespace HSNHospitalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
