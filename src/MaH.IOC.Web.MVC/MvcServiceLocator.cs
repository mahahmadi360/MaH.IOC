using System.Linq;
using System.Web.Mvc;

namespace MaH.IOC.Web.MVC
{
    public static class MvcServiceResolverLocator
    {
        //Adds Mah IOC as dependency resolver in ASP.Net MVC project
        public static void SetDependencyResolver()
        {
            RegisterFilterAttributeProvider();

            DependencyResolver.SetResolver(new MvcDependencyResolver());
        }

        private static void RegisterFilterAttributeProvider()
        {
            var defaultFilterProvider = FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().FirstOrDefault();
            if (defaultFilterProvider != null)
                FilterProviders.Providers.Remove(defaultFilterProvider);

            var hmswFilterProvider = FilterProviders.Providers.OfType<MaHFilterAttributeFilterProvider>().FirstOrDefault();
            if (hmswFilterProvider == null)
                FilterProviders.Providers.Add(new MaHFilterAttributeFilterProvider());
        }
    }
}
