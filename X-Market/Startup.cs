using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(X_Market.Startup))]
namespace X_Market
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
