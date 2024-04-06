using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace NTestTemplate.IsolatedSetup;

[SetUpFixture]
public class TestSetup
{
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
    private static IServiceProvider _serviceProvider;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

    [OneTimeSetUp]
    public void OTSetup()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddSingleton(typeof(ILogger), Log.Logger);
        _serviceProvider = services.BuildServiceProvider();
    }
    [OneTimeTearDown]
    public void OTTearDown()
    {
        (_serviceProvider as IDisposable)?.Dispose();
    }

    public static IServiceProvider GetServiceProvider()
    {
        return _serviceProvider;
    }

}