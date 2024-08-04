

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(souqcomApp.Startup))]
namespace souqcomApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
