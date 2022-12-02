namespace DomainServices.UnitTest;

public class NativCommunicatorUnitTest
{
    internal static string validatorPath()
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
