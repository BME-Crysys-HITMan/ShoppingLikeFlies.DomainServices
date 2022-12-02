using DomainServices.UnitTest.Scenarios;
using FluentAssertions.Extensions;
using ShoppingLikeFiles.DomainServices.Model;

namespace DomainServices.UnitTest;

public class CaffValidatorUnitTest
{
    public class CaffValidatorCtorUnitTests
    {
        [Fact]
        public void Test_ShouldThrow_ArgumentNull_1()
        {
            ICaffValidator validator;
            Action action = () => validator = new DefaultCaffValidator(null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Test_ShouldThrow_ArgumentNull_2()
        {
            string? arg = null;
            Mock<INativeCommunicator> communicator = new Mock<INativeCommunicator>();
            ICaffValidator validator;
            Action action = () => validator = new DefaultCaffValidator(communicator.Object, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Test_ShouldThrow_ArgumentNull_3()
        {
            ICaffValidator validator;
            ILogger logger = new LoggerConfiguration().CreateLogger();
            Action act = () => validator = new DefaultCaffValidator(null, logger);

            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Test_ShouldNotThrow()
        {
            Mock<INativeCommunicator> communicator = new Mock<INativeCommunicator>();
            ILogger logger = new LoggerConfiguration().CreateLogger();
            ICaffValidator validator;

            Action act = () => validator = new DefaultCaffValidator(communicator.Object, logger);

            act.Should().NotThrow<ArgumentNullException>();
        }
    }

    public class CaffValidatorSyncUnitTest
    {
        [Fact]
        public void Test1()
        {
            string fileName = "testFile.caff";
            string expectedReturn = "";
            ILogger logger = new LoggerConfiguration().CreateLogger().ForContext<CaffValidatorAsyncUnitTest>();

            var scenario = new CaffValidationScenario(expectedReturn, fileName, logger);

            Action action = () => scenario.Validator.ValidateFile("");

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Test2()
        {
            string fileName = "testFile.caff";
            string expectedReturn = "0\r\n";
            ILogger logger = new LoggerConfiguration().CreateLogger().ForContext<CaffValidatorAsyncUnitTest>();

            var scenario = new CaffValidationScenario(expectedReturn, fileName, logger);

            var result = scenario.Validator.ValidateFile("notexistingfile.caff");

            result.Should().BeNull();
        }

        [Fact]
        public void Test3()
        {
            string fileName = "testFile.caff";
            var expectedDate = new DateTime(2020, 7, 2, 14, 50, 0);
            string expectedReturn = "1\r\n2020:7:2:14:50\r\nTest Creator\r\nlandscape;mountains;sunset;";
            ILogger logger = new LoggerConfiguration().CreateLogger().ForContext<CaffValidatorAsyncUnitTest>();
            var scenario = new CaffValidationScenario(expectedReturn, fileName, logger);


            var result = scenario.Validator.ValidateFile(fileName);

            result.Should().NotBeNull();
            result.As<CaffCredit>().CreationDate.Should().BeCloseTo(expectedDate, TimeSpan.FromMinutes(1));
            result.As<CaffCredit>().Creator.Should().Be("Test Creator");
            result.As<CaffCredit>().Tags.Should().OnlyHaveUniqueItems().And.HaveCount(3);
        }
    }

    public class CaffValidatorAsyncUnitTest
    {
        [Fact]
        public async Task Test1()
        {
            string fileName = "testFile.caff";
            string expectedReturn = "";
            ILogger logger = new LoggerConfiguration().CreateLogger().ForContext<CaffValidatorAsyncUnitTest>();

            var scenario = new CaffValidationScenario(expectedReturn, fileName, logger);

            Func<Task> act = () => scenario.Validator.ValidateFileAsync("");

            await act.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Test2()
        {
            string fileName = "testFile.caff";
            string expectedReturn = "0\r\n";
            ILogger logger = new LoggerConfiguration().CreateLogger().ForContext<CaffValidatorAsyncUnitTest>();

            var scenario = new CaffValidationScenario(expectedReturn, fileName, logger);

            Func<Task<CaffCredit?>> act = () => scenario.Validator.ValidateFileAsync("nonexisting.caff");

            await act.Should().CompleteWithinAsync(5.Minutes()).WithResult(null);
        }

        [Fact]
        public async Task Test3()
        {
            string fileName = "testFile.caff";
            var expectedDate = new DateTime(2020, 7, 2, 14, 50, 0);
            string expectedReturn = "1\r\n2020:7:2:14:50\r\nTest Creator\r\nlandscape;mountains;sunset;";
            ILogger logger = new LoggerConfiguration().CreateLogger().ForContext<CaffValidatorAsyncUnitTest>();

            var scenario = new CaffValidationScenario(expectedReturn, fileName, logger);

            var actual = await scenario.Validator.ValidateFileAsync("testFile.caff");
            actual.Should().NotBeNull();

            actual.As<CaffCredit>().CreationDate.Should().BeCloseTo(expectedDate, TimeSpan.FromMinutes(1));
            actual.As<CaffCredit>().Creator.Should().Be("Test Creator");
            actual.As<CaffCredit>().Tags.Should().OnlyHaveUniqueItems().And.HaveCount(3);
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

    private static string validatorPath()
    {
        var cwd = Directory.GetCurrentDirectory();

        var files = Directory.EnumerateFiles(cwd, "CAFF_Proc*");

        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(osPlatform: System.Runtime.InteropServices.OSPlatform.Windows))
        {
            if (files.Any(f => f.EndsWith(".exe")))
            {
                return files.First(f => f.EndsWith(".exe"));
            }

            throw new Exception("Validator not found!");
        }

        var file = files.FirstOrDefault(f => !f.EndsWith(".exe"));

        if (string.IsNullOrEmpty(file))
        {
            throw new Exception("Validator not found!");
        }

        return file;
        //return "Z:\\BME\\MSc\\SzamBiz\\ShoppingLikeFiles-NativeComponent\\cmake-build-debug\\CAFF_Processor.exe";
    }
}
