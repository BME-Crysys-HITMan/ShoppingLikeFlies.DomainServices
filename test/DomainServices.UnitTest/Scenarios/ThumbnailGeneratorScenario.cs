
namespace DomainServices.UnitTest.Scenarios;

internal class ThumbnailGeneratorScenario
{
    private readonly IThumbnailGenerator generator;
    private readonly Mock<IUploadService> upload;

    public ThumbnailGeneratorScenario(string expectedFileName)
    {
        upload = new Mock<IUploadService>();

        upload.Setup(x => x.UploadFileAsync(It.IsAny<byte[]>())).Returns(Task.FromResult(expectedFileName));
        upload.Setup(x => x.UploadFile(It.IsAny<byte[]>())).Returns(expectedFileName);

        var cwd = Directory.GetCurrentDirectory();

        string genDir = cwd + "/generator";

        Directory.CreateDirectory(genDir);

        var dirs = Directory.GetDirectories(cwd, "generator");

        generator = new DefaultThumbnailGenerator(validatorPath(), dirs.First(), upload.Object);
    }

    public IThumbnailGenerator Generator => generator;
    public Mock<IUploadService> Upload => upload;

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
