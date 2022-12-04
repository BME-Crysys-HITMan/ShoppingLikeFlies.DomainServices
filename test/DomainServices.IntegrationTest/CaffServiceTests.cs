using DomainServices.IntegrationTest.Scenario;
using FluentAssertions;
using Serilog;

namespace DomainServices.IntegrationTest;

[CollectionDefinition(nameof(CaffServiceTests), DisableParallelization = true)]
public class CaffServiceTests
{
    [Fact]
    public async Task Test1()
    {
        var logger = new LoggerConfiguration()
            .Enrich.WithProperty("testrunner", nameof(Test1))
            .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File($"{typeof(CaffServiceTests).FullName}.{nameof(Test1)}.txt")
                .MinimumLevel
                .Verbose()
                .MinimumLevel
                .Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();

        var scenario = new DomainTestScenario(logger, dbName: "Caff.Test1");

        var service = scenario.CaffService;
        string file = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "NativeFiles" + Path.DirectorySeparatorChar + "validfile.caff";
        int? id = await service.UploadFileAsync(file);

        id.Should().NotBeNull();
        id.Should().HaveValue();

        var data = scenario.DataService;

        var caff = await data.GetCaffAsync(id.Value);

        caff.Should().NotBeNull();

        File.Exists(caff.FilePath).Should().BeTrue();
        File.Exists(caff.ThumbnailPath).Should().BeTrue();
    }

    [Fact]
    public async Task Test2()
    {
        var logger = new LoggerConfiguration()
            .Enrich.WithProperty("testrunner", nameof(Test2))
            .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File($"{typeof(CaffServiceTests).FullName}.{nameof(Test2)}.txt")
                .MinimumLevel
                .Verbose()
                .MinimumLevel
                .Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();

        var scenario = new DomainTestScenario(logger, dbName: "Caff.Test1");

        var service = scenario.CaffService;
        string file = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "NativeFiles" + Path.DirectorySeparatorChar + "invalid.caff";
        int? id = await service.UploadFileAsync(file);

        id.Should().BeNull();
        id.Should().NotHaveValue();
    }
}
