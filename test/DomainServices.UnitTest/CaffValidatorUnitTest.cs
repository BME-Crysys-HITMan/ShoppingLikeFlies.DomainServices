using FluentAssertions.Extensions;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Core;
using ShoppingLikeFiles.DomainServices.Core.Internal;
using ShoppingLikeFiles.DomainServices.Options;

namespace DomainServices.UnitTest;

public class CaffValidatorUnitTest
{
    public class CaffValidatorCtorUnitTests
    {
        [Fact]
        public void Test_ShouldThrow_ArgumentNull_1()
        {
            ICaffValidator validator;
            Action action = () => validator = new DefaultCaffValidator("");

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Test_ShouldThrow_ArgumentNull_2()
        {
            string? arg = null;
            ICaffValidator validator;
            Action action = () => validator = new DefaultCaffValidator(arg);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Test_ShouldThrow_ArgumentNull_3()
        {
            IOptions<CaffValidatorOptions>? options = null;
            ICaffValidator validator;
            Action act = () => validator = new DefaultCaffValidator(options);

            act.Should().ThrowExactly<ArgumentNullException>();
        }
        [Fact]
        public void Test_ShouldThrow_ArgumentNull_4()
        {
            IOptions<CaffValidatorOptions> options = Options.Create(new CaffValidatorOptions() { Validator = null });
            ICaffValidator validator;
            Action act = () => validator = new DefaultCaffValidator(options);

            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Test_ShouldNotThrow()
        {
            IOptions<CaffValidatorOptions> options = Options.Create(new CaffValidatorOptions() { Validator = "Valami" });
            ICaffValidator validator;
            Action act = () => validator = new DefaultCaffValidator(options);

            act.Should().NotThrow<ArgumentNullException>();
        }
    }

    public class CaffValidatorSyncUnitTest
    {
        [Fact]
        public void Test1()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());

            Action action = () => validator.ValidateFile("");

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Test2()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());

            bool result = validator.ValidateFile("notexistingfile.caff");

            result.Should().Be(false, "Nonexistant file should not be valid");
        }

        [Fact]
        public void Test3()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());
            var fileName = GetFile("invalid.caff");
            bool result = validator.ValidateFile(fileName);

            result.Should().Be(false, "Invalid file should not be valid");
        }

        [Fact]
        public void Test4()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());

            var fileName = GetFile("validfile.caff");
            bool result = validator.ValidateFile(fileName);

            result.Should().Be(true, "A valid file should not be invalid");
        }

        [Fact]
        public void Test5()
        {
            ICaffValidator validator = new DefaultCaffValidator("nonexistant_validator");

            bool result = validator.ValidateFile("notexistingfile.caff");

            result.Should().Be(false, "Nonexistant file should not be valid");
        }
    }

    public class CaffValidatorAsyncUnitTest
    {

        [Fact]
        public async Task Test1()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());

            Func<Task> act = () => validator.ValidateFileAsync("");

            await act.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Test2()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());

            Func<Task<bool>> act = () => validator.ValidateFileAsync("nonexisting.caff");

            await act.Should().CompleteWithinAsync(500.Milliseconds()).WithResult(false);
        }

        [Fact]
        public async Task Test3()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());

            Func<Task<bool>> act = () => validator.ValidateFileAsync(GetFile("invalid.caff"));

            await act.Should().CompleteWithinAsync(500.Milliseconds()).WithResult(false);
        }

        [Fact]
        public async Task Test4()
        {
            ICaffValidator validator = new DefaultCaffValidator(validatorPath());

            Func<Task<bool>> act = () => validator.ValidateFileAsync(GetFile("validfile.caff"));

            await act.Should().CompleteWithinAsync(500.Milliseconds()).WithResult(true);
        }

        [Fact]
        public async Task Test5()
        {
            ICaffValidator validator = new DefaultCaffValidator("nonexistant_validator");

            bool result = await validator.ValidateFileAsync("notexistingfile.caff");

            result.Should().Be(false, "Nonexistant file should not be valid");
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
