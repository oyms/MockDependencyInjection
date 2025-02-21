using Microsoft.Extensions.Logging;
using Moq;

namespace Skaar.MockDependencyInjection.Moq.Tests.Fixture;

public class LogSinkTests(ITestOutputHelper @out)
{
    [Fact]
    public void AddLogSink_WhenLogging_WritesToSink()
    {
        var expected = "Testing log";
        var sink = new Mock<ISink>();
        sink.Setup(x => x.Write(It.IsAny<string>()))
            .Callback<string>(@out.WriteLine);

        var fixture = IoC
            .CreateFixture<TestTarget>()
            .UseLogSink(sink.Object.Write);
        
        var result = fixture.Resolve();
        result.WriteToLog(expected);
        
        sink.Verify(x => x.Write(It.Is<string>(t => t.Contains(expected))));
    }

    public interface ISink
    {
        void Write(string text);
    }
}

file class TestTarget(ILogger<TestTarget> logger)
{
    public void WriteToLog(string txt) => logger.LogInformation(txt);
}