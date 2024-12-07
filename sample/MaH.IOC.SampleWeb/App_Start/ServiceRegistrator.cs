using MaH.IOC.SampleWeb.Services;

namespace MaH.IOC.SampleWeb.App_Start
{
    public static class ServiceRegistrator
    {
        public static void Register()
        {
            var services = IocContainer.Instance;

            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IScopedService, ScopedService>();
        }
    }
}