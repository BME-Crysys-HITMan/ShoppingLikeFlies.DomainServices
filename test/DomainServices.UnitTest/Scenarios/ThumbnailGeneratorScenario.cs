
namespace DomainServices.UnitTest.Scenarios;

internal class ThumbnailGeneratorScenario : IDisposable
{
    private readonly string generatedFile;
    private readonly IThumbnailGenerator generator;
    private readonly Mock<IUploadService> upload;
    private readonly Mock<INativeCommunicator> nativeCommunicator;
    /// <summary>
    /// Scenario constructor.
    /// </summary>
    /// <param name="expectedFileName"> a jpeg image path</param>
    public ThumbnailGeneratorScenario(string originalName, string expectedFileName, string generatorDir, ILogger logger)
    {
        upload = new Mock<IUploadService>();

        upload.Setup(x => x.UploadFileAsync(It.IsAny<byte[]>(), It.IsAny<string>())).Returns(Task.FromResult(expectedFileName));
        upload.Setup(x => x.UploadFile(It.IsAny<byte[]>(), It.IsAny<string>())).Returns(expectedFileName);

        nativeCommunicator = new Mock<INativeCommunicator>();

        var pixel = expectedFileName[..^5];

        nativeCommunicator.Setup(x => x.Communicate(It.IsAny<string>())).Returns<string?>(null);
        nativeCommunicator.Setup(x => x.Communicate(It.Is<string>(s => s.Contains(originalName) && s.Contains(generatorDir)))).Returns(pixel);
        nativeCommunicator.Setup(x => x.CommunicateAsync(It.IsAny<string>())).Returns<Task<string?>>(null);
        nativeCommunicator.Setup(x => x.CommunicateAsync(It.Is<string>(s => s.Contains(originalName) && s.Contains(generatorDir)))).Returns(Task.FromResult<string?>(pixel));

        generator = new DefaultThumbnailGenerator(nativeCommunicator.Object, generatorDir, upload.Object, logger);

        generatedFile = GeneratePixelFile(generatorDir, pixel);
    }

    public IThumbnailGenerator Generator => generator;

    private static string GeneratePixelFile(string dir, string pixel)
    {
        using BinaryWriter bw = new BinaryWriter(File.Open(dir + "\\" + pixel, FileMode.Create, FileAccess.Write));
        ulong width = 10, height = 10;
        byte[] w = BitConverter.GetBytes(width);
        byte[] h = BitConverter.GetBytes(height);
        bw.Write(w);
        bw.Write(h);
        Random r = new Random();
        for (int i = 0; i < 10; ++i)
        {
            bw.Write((byte)r.Next(256));
            bw.Write((byte)r.Next(256));
            bw.Write((byte)r.Next(256));
        }

        var files = Directory.GetFiles(dir, pixel);

        return files.First();
    }

    public void Dispose()
    {
        File.Delete(generatedFile);
    }
}
