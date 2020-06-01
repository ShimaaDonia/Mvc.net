using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_CommerceProject.Startup))]
namespace E_CommerceProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
