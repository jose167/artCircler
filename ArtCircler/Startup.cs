using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtCircler.Startup))]
namespace ArtCircler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
