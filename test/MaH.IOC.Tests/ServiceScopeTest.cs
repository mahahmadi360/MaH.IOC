using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MaH.IOC.Tests
{
	[TestClass]
	public class ServiceScopeTest
	{
		private IServiceCollection _serviceCollection;

		[TestInitialize]
		public void Initialize()
		{
			_serviceCollection = IocContainer.CreateInstance();
			_serviceCollection.AddScoped<TestService>();
		}

		[TestMethod]
		public void CreateScope_ScopeFomScope_DoesNotDisposeChildScope()
		{
			var baseServiceProvider = _serviceCollection.BuildServiceProvider();
			var scopeLevel1 = baseServiceProvider.CreateScope();
			var serviceProviderLevel1 = scopeLevel1.ServiceProvider;
			var scopeLevel2 = serviceProviderLevel1.CreateScope();
			var serviceProviderLevel2 = scopeLevel2.ServiceProvider;

			var serviceLevel1 = serviceProviderLevel1.GetService<TestService>();
			var serviceLevel2 = serviceProviderLevel2.GetService<TestService>();
			scopeLevel1.Dispose();

			Assert.IsTrue(serviceLevel1.Disposed);
			Assert.IsFalse(serviceLevel2.Disposed);
		}

		[TestMethod]
		public void CreateScope_BaseScopeDisposed_DisposeBothScopes()
		{
			var baseServiceProvider = _serviceCollection.BuildServiceProvider();
			var scopeLevel1 = baseServiceProvider.CreateScope();
			var serviceProviderLevel1 = scopeLevel1.ServiceProvider;
			var scopeLevel2 = serviceProviderLevel1.CreateScope();
			var serviceProviderLevel2 = scopeLevel2.ServiceProvider;

			var serviceLevel1 = serviceProviderLevel1.GetService<TestService>();
			var serviceLevel2 = serviceProviderLevel2.GetService<TestService>();
			baseServiceProvider.Dispose();

			Assert.IsTrue(serviceLevel1.Disposed);
			Assert.IsTrue(serviceLevel2.Disposed);
		}

		[TestCleanup]
		public void CleanUp()
		{
			_serviceCollection.Dispose();
		}

		private class TestService : IDisposable
		{
			public bool Disposed { get; set; }
			public void Dispose()
			{
				Disposed = true;
			}
		}
	}
}
