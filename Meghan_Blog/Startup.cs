using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Meghan_Blog.Startup))]
namespace Meghan_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
