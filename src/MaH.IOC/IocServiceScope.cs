using Unity;

namespace MaH.IOC {
	public class IocServiceScope : IServiceScope {
		private bool _disposed;
		public IServiceProvider ServiceProvider { get; }
		public IocServiceScope(IUnityContainer container) {
			var baseContainer = GetBaseContainer(container);
			var scopeContainer = baseContainer.CreateChildContainer();
			var serviceProvider = new IocServiceProvider(scopeContainer);

			scopeContainer.RegisterInstance<IServiceProvider>(serviceProvider);

			ServiceProvider = serviceProvider;
		}

		private IUnityContainer GetBaseContainer(IUnityContainer container) {
			while (container.Parent != null) {
				container = container.Parent;
			}
			return container;
		}

		public void Dispose() {
			if (_disposed) return;
			_disposed = true;
			ServiceProvider.Dispose();
		}
	}
}
