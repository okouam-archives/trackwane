using Microsoft.Owin;
using Owin;
using Trackwane.Data.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace Trackwane.Data.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
   
        }
    }
}
