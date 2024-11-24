using System;
using Unity;
using Unity.Lifetime;

namespace MaH.IOC {
	public class IocContainer : IServiceCollection {
		private IocContainer() {
			_container = new UnityContainer();
        }

		private bool _disposed;

		public static readonly IServiceCollection Instance = CreateInstance();
		/// <summary>
		/// Create an instance of IOC container, just use it in test senarios. Use instance instead to follow the singelton pattern
		/// </summary>
		internal static IServiceCollection CreateInstance() { return new IocContainer(); }


		private IUnityContainer _container;
		private IServiceProvider _serviceProvider;
		private object _lock = new object();


		public IServiceProvider BuildServiceProvider() {
			lock (_lock) {
				if (_serviceProvider == null) {
					_serviceProvider = new IocServiceProvider(_container);

					Add<IServiceProvider>(() => {
						return _serviceProvider;
					}, ServiceLifetime.Scoped);
				}

				return _serviceProvider.GetService<IServiceProvider>();
			}
		}

		public void Dispose() {
			if (_disposed) return;

			_disposed = true;
			_container?.Dispose();
			_serviceProvider?.Dispose();
			_container = null;
		}

		void IServiceCollection.Add<TService, TImplementation>(ServiceLifetime lifeStyle) {
			var registerName = GenerateRandomName();
			_container.RegisterType<TService, TImplementation>(registerName, CastTypeLifeStyle(lifeStyle));
			_container.RegisterFactory<TService>(container => container.Resolve<TService>(registerName), CastFactoryLifetime(lifeStyle));
		}

		void IServiceCollection.Add<TService>(TService instance) where TService : class {
			var registerName = GenerateRandomName();
			_container.RegisterInstance<TService>(registerName, instance, InstanceLifetime.Singleton);
			_container.RegisterFactory<TService>(container => container.Resolve<TService>(registerName));
		}

		void IServiceCollection.Add<TService>(System.Func<IServiceProvider, TService> factoryMethod, ServiceLifetime serviceLifetime) where TService : class {
			var registerName = GenerateRandomName();
			_container.RegisterFactory<TService>(registerName, (container) => factoryMethod(container.Resolve<IServiceProvider>()), CastFactoryLifetime(serviceLifetime));
			_container.RegisterFactory<TService>((container) => container.Resolve<TService>(registerName), CastFactoryLifetime(serviceLifetime));
		}

		void IServiceCollection.Add(Type serviceType, Type implementationType, ServiceLifetime lifeStyle) {
			var registerName = GenerateRandomName();
			_container.RegisterType(serviceType, implementationType, registerName, CastTypeLifeStyle(lifeStyle));
			_container.RegisterType(serviceType, implementationType, CastTypeLifeStyle(lifeStyle));
		}

		private void Add<TService>(System.Func<TService> factoryMethod, ServiceLifetime serviceLifetime) where TService : class {
			_container.RegisterFactory<TService>((container) => factoryMethod(), CastFactoryLifetime(serviceLifetime));
		}

		void IServiceCollection.Add<TService>(ServiceLifetime lifeStyle) where TService : class {
			var registerName = GenerateRandomName();
			_container.RegisterType<TService>(registerName, CastTypeLifeStyle(lifeStyle));
			_container.RegisterFactory<TService>((container) => container.Resolve<TService>(registerName), CastFactoryLifetime(lifeStyle));
		}

		/// <summary>
		/// unity doesn't support multi injection without names, then we create a random name for each registeration
		/// </summary>
		public string GenerateRandomName() {
			return Guid.NewGuid().ToString("N");
		}

		private IFactoryLifetimeManager CastFactoryLifetime(ServiceLifetime serviceLifetime) {
			switch (serviceLifetime) {
				case ServiceLifetime.Singelton:
					return FactoryLifetime.Singleton;
				case ServiceLifetime.Scoped:
					return new HierarchicalLifetimeManager();
				case ServiceLifetime.Transient:
					return FactoryLifetime.Transient;
				default:
					return FactoryLifetime.Transient;
			}
		}

		private ITypeLifetimeManager CastTypeLifeStyle(ServiceLifetime lifeStyle) {
			switch (lifeStyle) {
				case ServiceLifetime.Singelton:
					return TypeLifetime.Singleton;
				case ServiceLifetime.Scoped:
					return new HierarchicalLifetimeManager();
				case ServiceLifetime.Transient:
					return TypeLifetime.Transient;
				default:
					return TypeLifetime.Transient;
			}
		}
	}
}
