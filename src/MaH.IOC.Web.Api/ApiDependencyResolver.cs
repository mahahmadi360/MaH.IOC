using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;

namespace MaH.IOC.Web.Api
{
    internal class ApiDependencyResolver : IDependencyResolver, IDependencyScope, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;

        public ApiDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDependencyScope BeginScope()
        {
            return new ApiDependencyResolver(HttpContextServiceLocator.ServiceProvider);
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }

        public object GetService(Type serviceType)
        {
            if (typeof(IHttpController).IsAssignableFrom(serviceType))
            {
                return _serviceProvider.GetService(serviceType);
            }

            try
            {
                return _serviceProvider.GetService(serviceType);
            }
            catch
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
