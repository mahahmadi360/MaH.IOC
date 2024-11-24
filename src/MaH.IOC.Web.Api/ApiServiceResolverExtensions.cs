using System.Web.Http;

namespace MaH.IOC.Web.Api
{
    public static class ApiServiceResolverExtensions
    {
        public static void RegisterIOC(this HttpConfiguration config)
        {
            config.DependencyResolver = new ApiDependencyResolver(HttpContextServiceLocator.ServiceProvider);
        }
    }
}
