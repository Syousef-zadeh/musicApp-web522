using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment_6.Startup))]

namespace Assignment_6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
