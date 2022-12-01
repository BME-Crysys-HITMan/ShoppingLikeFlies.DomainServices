using FluentAssertions.Extensions;
using ShoppingLikeFiles.DomainServices.Core;
using ShoppingLikeFiles.DomainServices.Core.Internal;

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
    }

    public class CaffValidatorSyncUnitTest
    {
        private string validatorPath()
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

        private string GetFile(string testname)
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

    public class CaffValidatorAsyncUnitTest
    {
        private string validatorPath()
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
        }

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
