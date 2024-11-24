using System.Web;

namespace MaH.IOC.Web
{
    public static class HttpContextServiceLocator
    {
        private const string _serviceProviderKey = "70fbafe4-437c-421a-9f0f-c833f8b8d496";
        private static object _serviceProviderLockKey = "291f0f83-bb16-4e7c-8ab4-96fa979b52e3";
        //Provides a unique service provider for each request
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (HttpContext.Current.Items.Contains(_serviceProviderKey))
                    return (IServiceProvider)HttpContext.Current.Items[_serviceProviderKey];

                var lockObject = HttpContext.Current.Items[_serviceProviderLockKey] ?? new object();
                HttpContext.Current.Items[_serviceProviderLockKey] = lockObject;

                lock (lockObject)
                {
                    if (HttpContext.Current.Items.Contains(_serviceProviderKey))
                        return (IServiceProvider)HttpContext.Current.Items[_serviceProviderKey];

                    var scope = IocContainer.Instance.BuildServiceProvider().CreateScope();
                    var serviceProvider = scope.ServiceProvider;
                    HttpContext.Current.Items.Add(_serviceProviderKey, serviceProvider);

                    HttpContext.Current.AddOnRequestCompleted((h) => scope.Dispose());
                    return serviceProvider;

                }
            }
        }
    }
}
