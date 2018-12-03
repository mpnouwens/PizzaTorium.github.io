using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaTorium_complete.Startup))]
namespace PizzaTorium_complete
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
