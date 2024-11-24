using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Unity;

namespace MaH.IOC.Web.MVC
{
    internal class MvcDependencyResolver : IDependencyResolver
    {
        private IServiceProvider _serviceProvider => HttpContextServiceLocator.ServiceProvider;

        public object GetService(Type serviceType)
        {
            if (typeof(IController).IsAssignableFrom(serviceType))
            {
                return _serviceProvider.GetService(serviceType);
            }

            try
            {
                return _serviceProvider.GetService(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }
    }
}
