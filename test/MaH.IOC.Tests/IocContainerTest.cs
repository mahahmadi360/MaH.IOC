using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using IServiceProvider = MaH.IOC.IServiceProvider;

namespace MaH.IOC.Tests
{

    [TestClass]
    public class IocContainerTest
    {
        private IServiceCollection _serviceCollection;

        [TestInitialize]
        public void Init()
        {
            _serviceCollection = IocContainer.CreateInstance();
        }

        [TestMethod]
        public void Instance_IsSingelton()
        {
            Assert.AreEqual(IocContainer.Instance, IocContainer.Instance);
            IocContainer.Instance.Dispose();
        }

        [TestMethod]
        public void BuildServiceProvider_SolveIServiceProvider_RetunSameInstance()
        {
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolvedSrviceProvider = servieProvider.GetService<IServiceProvider>();
            Assert.AreEqual(servieProvider, resolvedSrviceProvider);
        }

        [TestMethod]
        public void BuildServiceProvider_ScopedServiceProvider_DiffentFromBaseServiceProvider()
        {
            var servieProvider = _serviceCollection.BuildServiceProvider();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var resolvedSrviceProvider = scopeServiceProvider.GetService<IServiceProvider>();

                Assert.AreEqual(scopeServiceProvider, resolvedSrviceProvider);
                Assert.AreNotEqual(servieProvider, resolvedSrviceProvider);
            }
        }


        interface ITranseintService { }
        class TranseintService : ITranseintService { }

        [TestMethod]
        public void ResolveTransient_WorksAsExpected()
        {
            _serviceCollection.AddTransient<ITranseintService, TranseintService>();
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<ITranseintService>();
            var resolve2 = servieProvider.GetService<ITranseintService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<ITranseintService>();

                Assert.AreNotEqual(resolve2, scopeResolve);
                Assert.AreNotEqual(resolve1, scopeResolve);
            }
            Assert.AreNotEqual(resolve1, resolve2);
        }

        class TranseintFactoryService { }

        [TestMethod]
        public void ResolveTransientFactory_WorksAsExpected()
        {
            _serviceCollection.AddTransient<TranseintFactoryService>((s) => new TranseintFactoryService());
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<TranseintFactoryService>();
            var resolve2 = servieProvider.GetService<TranseintFactoryService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<TranseintFactoryService>();

                Assert.AreNotEqual(resolve2, scopeResolve);
                Assert.AreNotEqual(resolve1, scopeResolve);
            }
            Assert.AreNotEqual(resolve1, resolve2);
        }


        interface IScopedService { }
        class ScopedService : IScopedService { }

        [TestMethod]
        public void ResolveScoped_WorksAsExpected()
        {
            _serviceCollection.AddScoped<IScopedService, ScopedService>();
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<IScopedService>();
            var resolve2 = servieProvider.GetService<IScopedService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<IScopedService>();
                var scopeResolve2 = scopeServiceProvider.GetService<IScopedService>();

                Assert.AreNotEqual(resolve1, scopeResolve);
                Assert.AreEqual(scopeResolve2, scopeResolve);
            }
            Assert.AreEqual(resolve1, resolve2);
        }

        interface IScopedFactoryService { }
        class ScopedFactoryService : IScopedFactoryService { }

        [TestMethod]
        public void ResolveScopedFactory_WorksAsExpected()
        {
            _serviceCollection.AddScoped<IScopedFactoryService>((s) => new ScopedFactoryService());
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<IScopedFactoryService>();
            var resolve2 = servieProvider.GetService<IScopedFactoryService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<IScopedFactoryService>();
                var scopeResolve2 = scopeServiceProvider.GetService<IScopedFactoryService>();

                Assert.AreNotEqual(resolve1, scopeResolve);
                Assert.AreEqual(scopeResolve2, scopeResolve);
            }
            Assert.AreEqual(resolve1, resolve2);
        }

        class ScopedTypeService { }

        [TestMethod]
        public void ResolveScopedType_WorksAsExpected()
        {
            _serviceCollection.AddScoped<ScopedTypeService>();
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<ScopedTypeService>();
            var resolve2 = servieProvider.GetService<ScopedTypeService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<ScopedTypeService>();
                var scopeResolve2 = scopeServiceProvider.GetService<ScopedTypeService>();

                Assert.AreNotEqual(resolve1, scopeResolve);
                Assert.AreEqual(scopeResolve2, scopeResolve);
            }
            Assert.AreEqual(resolve1, resolve2);
        }


        interface ISingeltonService { }
        class SingltonService : ISingeltonService { }

        [TestMethod]
        public void ResolveSingelton_WorksAsExpected()
        {
            _serviceCollection.AddSingelton<ISingeltonService, SingltonService>();
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<ISingeltonService>();
            var resolve2 = servieProvider.GetService<ISingeltonService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<ISingeltonService>();
                var scopeResolve2 = scopeServiceProvider.GetService<ISingeltonService>();

                Assert.AreEqual(resolve1, scopeResolve);
                Assert.AreEqual(scopeResolve2, scopeResolve);
            }
            Assert.AreEqual(resolve1, resolve2);
        }

        interface ISingeltonFactoryService { }
        class SingltonFactoryService : ISingeltonFactoryService { }

        [TestMethod]
        public void ResolveSingeltonFactory_WorksAsExpected()
        {
            _serviceCollection.AddSingelton<ISingeltonFactoryService>((s) => new SingltonFactoryService());
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<ISingeltonFactoryService>();
            var resolve2 = servieProvider.GetService<ISingeltonFactoryService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<ISingeltonFactoryService>();
                var scopeResolve2 = scopeServiceProvider.GetService<ISingeltonFactoryService>();

                Assert.AreEqual(resolve1, scopeResolve);
                Assert.AreEqual(scopeResolve2, scopeResolve);
            }
            Assert.AreEqual(resolve1, resolve2);
        }

        interface ISingeltonInstanceService { }
        class SingltonInstanceService : ISingeltonInstanceService { }

        [TestMethod]
        public void ResolveSingeltonInstance_WorksAsExpected()
        {
            var instance = new SingltonInstanceService();
            _serviceCollection.AddSingelton<ISingeltonInstanceService>((s) => instance);
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<ISingeltonInstanceService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<ISingeltonInstanceService>();

                Assert.AreEqual(resolve1, scopeResolve);
            }
            Assert.AreEqual(resolve1, instance);
        }

        class SingltonTypeService { }

        [TestMethod]
        public void ResolveSingeltonType_WorksAsExpected()
        {
            _serviceCollection.AddSingelton<SingltonTypeService>();
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<SingltonTypeService>();
            var resolve2 = servieProvider.GetService<SingltonTypeService>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<SingltonTypeService>();

                Assert.AreEqual(resolve1, scopeResolve);
            }
            Assert.AreEqual(resolve1, resolve2);
        }

        class CheckProviderService { }
        [TestMethod]
        public void ResolveWithGenericOrType_ReturnTheSameInstance()
        {
            _serviceCollection.AddSingelton<CheckProviderService>();
            var servieProvider = _serviceCollection.BuildServiceProvider();
            var resolve1 = servieProvider.GetService<CheckProviderService>();
            var resolve2 = servieProvider.GetService(typeof(CheckProviderService));
            Assert.AreEqual(resolve1, resolve2);
        }

        interface IMultipleRegisteredService { }
        class MultipleRegisteredService1 : IMultipleRegisteredService { }
        class MultipleRegisteredService2 : IMultipleRegisteredService { }
        class MultipleRegisteredService3 : IMultipleRegisteredService { }
        class MultipleRegisteredService4 : IMultipleRegisteredService { }
        class MultipleRegisteredService5 : IMultipleRegisteredService { }
        [TestMethod]
        public void ResolveMultipelRegistered_ItWorksAsExpected()
        {
            _serviceCollection.AddScoped<IMultipleRegisteredService, MultipleRegisteredService1>();
            _serviceCollection.AddScoped<IMultipleRegisteredService>((s) => new MultipleRegisteredService2());
            _serviceCollection.AddScoped<IMultipleRegisteredService, MultipleRegisteredService3>();
            _serviceCollection.AddScoped<IMultipleRegisteredService>((s) => new MultipleRegisteredService4());
            _serviceCollection.AddScoped<IMultipleRegisteredService, MultipleRegisteredService5>();
            var servieProvider = _serviceCollection.BuildServiceProvider();

            var resolve = servieProvider.GetService<IMultipleRegisteredService>();
            var resolves = servieProvider.GetServices<IMultipleRegisteredService>();

            using (var scope = servieProvider.CreateScope())
            {
                var scopedResolve = scope.ServiceProvider.GetService<IMultipleRegisteredService>();
                var scopedResolves = scope.ServiceProvider.GetServices<IMultipleRegisteredService>();

                //the single resolve is equals to the last registeration
                Assert.AreEqual(typeof(MultipleRegisteredService5), scopedResolve.GetType());
                //resolve multiple registerationm returns all registered types
                Assert.AreEqual(5, scopedResolves.Count());
                Assert.IsTrue(scopedResolves.Any(a => a.GetType() == typeof(MultipleRegisteredService1)));
                Assert.IsTrue(scopedResolves.Any(a => a.GetType() == typeof(MultipleRegisteredService2)));
                Assert.IsTrue(scopedResolves.Any(a => a.GetType() == typeof(MultipleRegisteredService3)));
                Assert.IsTrue(scopedResolves.Any(a => a.GetType() == typeof(MultipleRegisteredService4)));
                Assert.IsTrue(scopedResolves.Any(a => a.GetType() == typeof(MultipleRegisteredService5)));
                //all the resolved items in the scope are totally different from main service provider resolves
                Assert.IsTrue(scopedResolves.All(a => !resolves.Contains(a)));
            }

            //the single resolve is equals to the last registeration
            Assert.AreEqual(typeof(MultipleRegisteredService5), resolve.GetType());
            //resolve multiple registerationm returns all registered types
            Assert.AreEqual(5, resolves.Count());
            Assert.IsTrue(resolves.Any(a => a.GetType() == typeof(MultipleRegisteredService1)));
            Assert.IsTrue(resolves.Any(a => a.GetType() == typeof(MultipleRegisteredService2)));
            Assert.IsTrue(resolves.Any(a => a.GetType() == typeof(MultipleRegisteredService3)));
            Assert.IsTrue(resolves.Any(a => a.GetType() == typeof(MultipleRegisteredService4)));
            Assert.IsTrue(resolves.Any(a => a.GetType() == typeof(MultipleRegisteredService5)));
            //the signle resolve already exists in the multiple resolve
            Assert.IsTrue(resolves.Any(a => a.Equals(resolve)));
        }

        interface ITypeRegisterSingelton { }
        class TypeRegisterSingelton : ITypeRegisterSingelton { }
        [TestMethod]
        public void RegisterType_OneImplementationRegistered_ReturnExpected()
        {
            _serviceCollection.AddSingelton(typeof(ITypeRegisterSingelton), typeof(TypeRegisterSingelton));
            var servieProvider = _serviceCollection.BuildServiceProvider();

            var resolve1 = servieProvider.GetService<ITypeRegisterSingelton>();
            var resolve2 = servieProvider.GetService<ITypeRegisterSingelton>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<ITypeRegisterSingelton>();

                Assert.AreEqual(resolve2, scopeResolve);
                Assert.AreEqual(resolve1, scopeResolve);
            }
            Assert.AreEqual(resolve1, resolve2);
        }

        interface IMultiTypeRegisterSingelton { }
        class MultiTypeRegisterSingelton1 : IMultiTypeRegisterSingelton { }
        class MultiTypeRegisterSingelton2 : IMultiTypeRegisterSingelton { }
        [TestMethod]
        public void RegisterType_MultipleImplementationRegistered_ReturnExpected()
        {
            _serviceCollection.AddSingelton(typeof(IMultiTypeRegisterSingelton), typeof(MultiTypeRegisterSingelton1));
            _serviceCollection.AddSingelton(typeof(IMultiTypeRegisterSingelton), typeof(MultiTypeRegisterSingelton2));
            var servieProvider = _serviceCollection.BuildServiceProvider();

            var resolve1 = servieProvider.GetService<IMultiTypeRegisterSingelton>();
            var resolves = servieProvider.GetServices<IMultiTypeRegisterSingelton>();
            using (var scope = servieProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;
                var scopeResolve = scopeServiceProvider.GetService<IMultiTypeRegisterSingelton>();

                Assert.AreEqual(resolve1, scopeResolve);
            }

            Assert.AreEqual(typeof(MultiTypeRegisterSingelton2), resolve1.GetType());
            Assert.AreEqual(2, resolves.Count());
            Assert.IsTrue(resolves.Any(a => a.GetType() == typeof(MultiTypeRegisterSingelton1)));
            Assert.IsTrue(resolves.Any(a => a.GetType() == typeof(MultiTypeRegisterSingelton2)));
        }

        interface IGenericTypeRegisterSingelton<T> { Type PropertyType { get; } }
        class GenericTypeRegisterSingelton<T> : IGenericTypeRegisterSingelton<T> { public Type PropertyType => typeof(T); }
        [TestMethod]
        public void RegisterType_GenericImplementationRegistered_ReturnExpected()
        {
            _serviceCollection.AddSingelton(typeof(IGenericTypeRegisterSingelton<>), typeof(GenericTypeRegisterSingelton<>));
            var servieProvider = _serviceCollection.BuildServiceProvider();

            var resolveInt = servieProvider.GetService<IGenericTypeRegisterSingelton<int>>();
            var resolveString = servieProvider.GetService<IGenericTypeRegisterSingelton<string>>();

            Assert.AreEqual(typeof(int), resolveInt.PropertyType);
            Assert.AreEqual(typeof(string), resolveString.PropertyType);
        }

        [TestMethod]
        public void RegisterType_GenericImplementationRegistered_ResolveInDifferentScopes()
        {
            _serviceCollection.AddSingelton(typeof(IGenericTypeRegisterSingelton<>), typeof(GenericTypeRegisterSingelton<>));
            var servieProvider = _serviceCollection.BuildServiceProvider();

            var scope1 = servieProvider.CreateScope();
            var resolveScope1 = scope1.ServiceProvider.GetService<IGenericTypeRegisterSingelton<int>>();
            scope1.Dispose();
            var scope2 = servieProvider.CreateScope();
            var resolveScope2 = scope2.ServiceProvider.GetService<IGenericTypeRegisterSingelton<int>>();
            scope2.Dispose();

            Assert.AreEqual(resolveScope1, resolveScope2);
        }


        [TestCleanup]
        public void CleanUp()
        {
            _serviceCollection.Dispose();
        }

    }
}
