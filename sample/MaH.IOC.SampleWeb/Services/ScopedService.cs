using System.Collections.Generic;

namespace MaH.IOC.SampleWeb.Services
{
    public interface IScopedService
    {
        IEnumerable<string> GetValues();
    }

    internal class ScopedService : IScopedService
    {
        public IEnumerable<string> GetValues()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return $"Value {i + 1}";
            }
        }
    }
}