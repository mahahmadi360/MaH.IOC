using MaH.IOC.SampleWeb.App_Start;
using MaH.IOC.Web.MVC;
using System.Web.Http;
using System.Web.Routing;

namespace MaH.IOC.SampleWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Add services
            ServiceRegistrator.Register();
            //Add MVC service resolver
            MvcServiceResolverLocator.SetDependencyResolver();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
