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
            string expected = "file.jpg";
            var scenario = new ThumbnailGeneratorScenario(expected);

            scenario.Generator.GenerateThumbnail(GetFile("validfile.caff")).Should().Be(expected);
        }
    }

    public class ThumbnailGeneratorAsyncTests
    {
        [Fact]
        public async Task Test1()
        {
            string expected = "file.jpg";
            var scenario = new ThumbnailGeneratorScenario(expected);

            Func<Task<string?>> act = () => scenario.Generator.GenerateThumbnailAsync(GetFile("validfile.caff"));

            await act.Should().CompleteWithinAsync(1.Seconds()).WithResult(expected);
        }
    }

    internal static string GetFile(string testname)
    {
        var cwd = Directory.GetCurrentDirectory();

        var files = Directory.EnumerateFiles(cwd, "*.caff");

        if (files.Any(f => f.EndsWith(testname)))
        {
            return files.First(f => f.EndsWith(testname));
        }

        return "";
    }
}
