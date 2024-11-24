using System;

namespace MaH.IOC {
	public interface IServiceScope : IDisposable {
		IServiceProvider ServiceProvider { get; }
	}
}
