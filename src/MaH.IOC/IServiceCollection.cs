using System;

namespace MaH.IOC {
	public interface IServiceCollection : IDisposable {
		IServiceProvider BuildServiceProvider();

		internal void Add<TService, TImplementation>(ServiceLifetime lifeStyle)
			where TImplementation : TService
			where TService : class;

		internal void Add<TService>(TService instance)
			where TService : class;

		internal void Add<TService>(ServiceLifetime lifeStyle)
			where TService : class;

		internal void Add<TService>(Func<IServiceProvider, TService> factoryMethod, ServiceLifetime lifeStyle)
			where TService : class;

		internal void Add(Type serviceType, Type implementationType, ServiceLifetime lifeStyle);
	}
}
