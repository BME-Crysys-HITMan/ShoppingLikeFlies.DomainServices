using DomainServices.UnitTest.Scenarios;
using FluentAssertions.Extensions;

namespace DomainServices.UnitTest;

public class ThumbnailGeneratorUnitTest
{
    public class ThumbnailGeneratorSyncTests
    {
        [Fact]
        public void Test1()
        {
            var logger = new LoggerConfiguration().CreateBootstrapLogger().ForContext<ThumbnailGeneratorSyncTests>();
            string original = "image.caff";
            string expected = "image.caff.pixel.jpeg";
            using var scenario = new ThumbnailGeneratorScenario(original, expected, ".", logger);

            scenario.Generator.GenerateThumbnail(original).Should().Be(expected);
        }
    }

    public class ThumbnailGeneratorAsyncTests
    {
        [Fact]
        public async Task Test1()
        {
            var logger = new LoggerConfiguration().CreateBootstrapLogger().ForContext<ThumbnailGeneratorSyncTests>();
            string original = "image1.caff";
            string expected = "image1.caff.pixel.jpeg";
            using var scenario = new ThumbnailGeneratorScenario(original, expected, ".", logger);

            Func<Task<string?>> act = () => scenario.Generator.GenerateThumbnailAsync(original);

            await act.Should().CompleteWithinAsync(5.Minutes()).WithResult(expected);
        }
    }
}
