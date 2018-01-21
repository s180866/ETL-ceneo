using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ETL_Ceneo.Startup))]
namespace ETL_Ceneo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
