
# MaH.IOC - Dependency Injection for .NET Framework

**MaH.IOC** is a lightweight dependency injection (DI) container for .NET Framework applications. It provides a syntax and structure inspired by .NET Core's built-in DI container, enabling developers to use modern DI practices in legacy .NET Framework projects.

## Features

- **Compatibility**: Designed for .NET Framework (v4.8).
- **Familiar Syntax**: Adopts the IServiceCollection and IServiceProvider patterns.
- **Multiple Lifetime Scopes**: Supports Singleton, Scoped, and Transient lifetimes.
- **Web Support**: Includes extensions for:
  - ASP.NET Web Forms
  - ASP.NET MVC
  - ASP.NET Web API
- **Ease of Use**: Simple setup and intuitive API.
- **Sample Projects**: Demonstrates integration with various frameworks.

## Project Structure

- **MaH.IOC**: Core DI container implementation.
- **MaH.IOC.Web**: Extensions for Web Service Locator.
- **MaH.IOC.Web.Forms**: Extensions for ASP.NET Web Forms.
- **MaH.IOC.Web.MVC**: Extensions for ASP.NET MVC.
- **MaH.IOC.Web.Api**: Extensions for ASP.NET Web API.
- **MaH.IOC.Tests**: Unit tests for the DI container.
- **MaH.IOC.SampleWeb**: Sample application demonstrating DI in a real-world scenario.

## Installation

Clone the repository and build the solution using Visual Studio 2019 or later.
```bash
git clone https://github.com/mahahmadi360/MaH.IOC
```

Or add the following nuget.config file to your solution and install packages

```bash
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="MaHPackages" value="https://nuget.pkg.github.com/mahahmadi360/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <MaHPackages>
      <add key="Username" value="mahahmadi360" />
      <add key="ClearTextPassword" value="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" />
    </MaHPackages>
  </packageSourceCredentials>
</configuration>
```


## Getting Started

### Registering Services

Register your services using `IServiceCollection`:

```csharp
IServiceCollection services = MaH.IOC.IocContainer.Instance;

// Add services
services.AddSingleton<IMyService, MyService>();
services.AddScoped<IOtherService, OtherService>();
services.AddTransient<IAnotherService, AnotherService>();

// Build service provider
var serviceProvider = services.BuildServiceProvider();
```

### Resolving Services

Resolve services through `IServiceProvider`:

```csharp
var myService = serviceProvider.GetService<IMyService>();
```

### ASP.NET Integration

#### Web Forms
Use [Inject] attribute to resolve dependencies in Web pages or controls
1. Use `InjectAttribute` for dependency injection in pages or controls:

   ```csharp
   [Inject]
   public IMyService MyService { get; set; }
   ```

2. Ensure `InjectablePage` or `InjectableUserControl` is used as the base class.

Or use HttpContextServiceLocator to resolve services.

   ```csharp
   MaH.IOC.Web.HttpContextServiceLocator.ServiceProvider.GetService<IMyService>();
   ```

#### MVC

Register service resolver in `Global.asax.cs`:

```csharp
protected void Application_Start()
{
    MaH.IOC.Web.MVC.MvcServiceResolverLocator.SetDependencyResolver();

    /*...*/
}
```

#### Web APIa

Configure DI in `WebApiConfig.cs`:

```csharp
using MaH.IOC.Web.Api;
public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.RegisterIOC();

            /*...*/
        }
    }
```

## Examples

The repository includes sample projects in the `sample` folder:

- **SampleWeb**: Demonstrates DI usage in a Web Forms, MVC and Web API project.

## Contributing

Contributions are welcome! Please fork the repository, create a feature branch, and submit a pull request.

