using System;

namespace MaH.IOC {
	public static class IServiceCollectionExtensions {
		public static void AddSingelton<TService, TImplementation>(this IServiceCollection serviceCollection)
			where TService : class
			where TImplementation : TService {
			serviceCollection.Add<TService, TImplementation>(ServiceLifetime.Singelton);
		}

		public static void AddSingelton<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, TService> factoryMethod)
			where TService : class {
			serviceCollection.Add<TService>(factoryMethod, ServiceLifetime.Singelton);
		}

		public static void AddSingelton<TService>(this IServiceCollection serviceCollection)
			where TService : class {
			serviceCollection.Add<TService>(ServiceLifetime.Singelton);
		}

		public static void AddSingelton<TService>(this IServiceCollection serviceCollection, TService instance)
			where TService : class {
			serviceCollection.Add<TService>(instance);
		}

		public static void AddScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
			where TService : class
			where TImplementation : TService {
			serviceCollection.Add<TService, TImplementation>(ServiceLifetime.Scoped);
		}

		public static void AddScoped<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, TService> factoryMethod)
			where TService : class {
			serviceCollection.Add<TService>(factoryMethod, ServiceLifetime.Scoped);
		}

		public static void AddScoped<TService>(this IServiceCollection serviceCollection)
			where TService : class {
			serviceCollection.Add<TService>(ServiceLifetime.Scoped);
		}

		public static void AddTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
			where TService : class
			where TImplementation : TService {
			serviceCollection.Add<TService, TImplementation>(ServiceLifetime.Transient);
		}

		public static void AddTransient<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, TService> factoryMethod)
			where TService : class {
			serviceCollection.Add<TService>(factoryMethod, ServiceLifetime.Transient);
		}

		public static void AddSingelton(this IServiceCollection serviceCollection, Type serviceType, Type implementationType) {
			serviceCollection.Add(serviceType, implementationType, ServiceLifetime.Singelton);
		}
	}
}
