using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace NTestTemplate.IsolatedSetup;

[TestFixture]
public class TestClass
{
    private ILogger _logger;
    [SetUp]
    public void Setup()
    {
        _logger = TestSetup.GetServiceProvider().GetRequiredService<ILogger>();
    }

    [Test]
    public void TestMethod_1()
    {
        _logger.Information("logging test1 from ILogger");
        _logger.Warning("logging test1 from ILogger");
        _logger.Error("logging test1 from ILogger");
        _logger.Debug("logging test1 from ILogger");
        _logger.Fatal("logging test1 from ILogger");
        TestContext.WriteLine("Logging test1 from test context");
        Assert.Pass();
    }
    [TearDown]
    public void TearDown()
    {
        (_logger as IDisposable)?.Dispose();
    }
}