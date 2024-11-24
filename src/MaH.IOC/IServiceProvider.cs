using System;
using System.Collections.Generic;

namespace MaH.IOC {
	public interface IServiceProvider : IDisposable {
		TService GetService<TService>();
		IEnumerable<TService> GetServices<TService>();
		object GetService(Type type);
		IEnumerable<object> GetServices(Type type);
		IServiceScope CreateScope();
	}
}
