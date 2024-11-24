using System.Collections.Generic;
using System.Linq;
using Unity;

namespace MaH.IOC {
	public class IocServiceProvider : IServiceProvider {
		private readonly IUnityContainer _container;
		private bool _disposed;

		public IocServiceProvider(IUnityContainer container) {
			_container = container;
		}

		public IServiceScope CreateScope() {
			return new IocServiceScope(_container);
		}
		public TService GetService<TService>() {
			return _container.Resolve<TService>();
		}

		public IEnumerable<TService> GetServices<TService>() {
			return GetServices(typeof(TService)).Cast<TService>();
		}

		public object GetService(System.Type type) {
			return _container.Resolve(type);
		}

		public IEnumerable<object> GetServices(System.Type type) {
			return _container.ResolveAll(type);
		}

		public void Dispose() {
			if (_disposed) return;
			_disposed = true;
			_container.Dispose();
		}
	}
}
