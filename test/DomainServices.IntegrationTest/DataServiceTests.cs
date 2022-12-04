using DomainServices.IntegrationTest.Scenario;
using FluentAssertions;
using Serilog;
using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace DomainServices.IntegrationTest
{
    public class DataServiceTests
    {
        [Fact]
        public async Task Test1()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"{typeof(DataServiceTests).FullName}.{nameof(Test1)}.txt")
                .MinimumLevel
                .Verbose()
                .MinimumLevel
                .Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();

            var scenario = new DomainTestScenario(logger);
            var service = scenario.DataService;

            var models = await service.GetAllAsync();

            Assert.NotNull(models);
            Assert.Empty(models);

            logger.Dispose();
        }

        [Fact]
        public async Task Test2()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"{typeof(DataServiceTests).FullName}.{nameof(Test2)}.txt")
                .MinimumLevel
                .Verbose()
                .MinimumLevel
                .Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();

            var scenario = new DomainTestScenario(logger);

            await scenario.SeedDb(DefaultCaffList);

            var service = scenario.DataService;

            var models = await service.GetAllAsync();

            models.Should().NotBeNull();
            models.Should().NotBeEmpty();

            models.Should().HaveCount(1);

            var model = await service.SearchCaffAsync(new ShoppingLikeFiles.DomainServices.DTOs.CaffSearchDTO { Creator = new() { models.First().Creator } });

            models.Should().NotBeNullOrEmpty();

            models.First().Creator.Should().Be(model.First().Creator);
            models.First().ThumbnailPath.Should().Be(model.First().ThumbnailPath);

            logger.Dispose();
        }

        private List<Caff> DefaultCaffList => new()
            {
                new(){Caption="hih",CreationDateTime=new DateTime(2020,1,1,1,1,0,DateTimeKind.Utc), Creator="haha",FilePath="Feri",Tags="ha;ha;hi;hi",ThumbnailPath="Bela"},
            };
    }
}
