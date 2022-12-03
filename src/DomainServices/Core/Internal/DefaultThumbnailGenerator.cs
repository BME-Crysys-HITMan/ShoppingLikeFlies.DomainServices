using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Exceptions;
using ShoppingLikeFiles.DomainServices.Options;
using SkiaSharp;

namespace ShoppingLikeFiles.DomainServices.Core.Internal;

internal class DefaultThumbnailGenerator : IThumbnailGenerator
{
    private readonly string _generatorDir;
    //private readonly ILogger //_logger;
    private readonly IUploadService _uploadService;
    private readonly INativeCommunicator _nativeCommunicator;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="communicator"></param>
    /// <param name="options"></param>
    /// <param name="uploadService"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DefaultThumbnailGenerator(
        INativeCommunicator communicator,
        IOptions<ThumbnailGeneratorOptions> options,
        IUploadService uploadService)//,
        //ILogger logger)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (options.Value is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        var x = options.Value;

        if (string.IsNullOrEmpty(x.GeneratorDir))
        {
            throw new ArgumentNullException(nameof(options));
        }

        _generatorDir = x.GeneratorDir;
        this._nativeCommunicator = communicator ?? throw new ArgumentNullException(nameof(communicator));
        _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
        ////_logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Constructor for testing porposes
    /// </summary>
    /// <param name="communicator"></param>
    /// <param name="generatorDir"></param>
    /// <param name="uploadService"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal DefaultThumbnailGenerator(
        INativeCommunicator communicator,
        string generatorDir,
        IUploadService uploadService)//,
        //ILogger logger)
    {
        this._nativeCommunicator = communicator ?? throw new ArgumentNullException(nameof(communicator));
        _generatorDir = generatorDir ?? throw new ArgumentNullException(nameof(generatorDir));
        _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
        ////_logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public string? GenerateThumbnail(
        string caffFile)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(GenerateThumbnail), caffFile);
        var cleanName = caffFile.Trim();
        if (string.IsNullOrEmpty(cleanName))
        {
            throw new ArgumentNullException(nameof(caffFile));
        }

        var pixelFile = GeneratePixelsFile(cleanName);

        if (string.IsNullOrEmpty(pixelFile))
        {
            ////_logger.Debug("Pixel could not be generated!");
            return null;
        }


        return ProcessPixel(pixelFile);
    }

    public async Task<string?> GenerateThumbnailAsync(
        string caffFile)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(GenerateThumbnailAsync), caffFile);
        if (string.IsNullOrEmpty(caffFile))
        {
            throw new ArgumentNullException(nameof(caffFile));
        }

        var pixelFile = await GeneratePixelsFileAsync(caffFile);

        if (string.IsNullOrEmpty(pixelFile))
        {
            ////_logger.Debug("Pixel could not be generated!");
            return null;
        }

        return await ProcessPixelAsync(pixelFile);
    }

    /// <summary>
    /// Generates a .pixel file from a caff image.
    /// </summary>
    /// <param name="caffFile"></param>
    /// <returns>Return null if pixel file could not be generated.</returns>
    private string? GeneratePixelsFile(
        string caffFile)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(GeneratePixelsFile), caffFile);
        return _nativeCommunicator.Communicate(GetArgs(caffFile));
    }

    /// <summary>
    /// Same as <see cref="GeneratePixelsFile"/> but async.
    /// </summary>
    /// <param name="caffFile"></param>
    /// <returns>Return null if pixel file could not be generated.</returns>
    private Task<string?> GeneratePixelsFileAsync(
        string caffFile)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(GeneratePixelsFileAsync), caffFile);
        return _nativeCommunicator.CommunicateAsync(GetArgs(caffFile));
    }

    /// <summary>
    /// Same as <see cref="ProcessPixel"/> but async.
    /// </summary>
    /// <param name="pixelFile"></param>
    /// <returns>Returns null is there was an error. Otherwise return the path to file.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    private async Task<string?> ProcessPixelAsync(
        string pixelFile)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(ProcessPixelAsync), pixelFile);
        if (string.IsNullOrEmpty(pixelFile))
        {
            throw new ArgumentNullException(nameof(pixelFile));
        }

        var data = GenerateJpeg(pixelFile);

        var filename = GetFileName(pixelFile);

        return await _uploadService.UploadFileAsync(data, filename);
    }

    /// <summary>
    /// Creates and saves a jpeg image from .pixel file.
    /// </summary>
    /// <param name="pixelFile"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private string? ProcessPixel(
        string pixelFile)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(ProcessPixel), pixelFile);
        if (string.IsNullOrEmpty(pixelFile))
        {
            throw new ArgumentNullException(nameof(pixelFile));
        }

        var data = GenerateJpeg(pixelFile);

        var fileName = GetFileName(pixelFile);

        ////_logger.Debug("Saving file as {fileName}", fileName);

        return _uploadService.UploadFile(data, fileName);
    }

    /// <summary>
    /// Generates a jpeg format data from pixel data.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="OverflowException"></exception>
    private byte[] GenerateJpeg(
        string filename)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(GenerateJpeg), filename);
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        using BinaryReader sr = new BinaryReader(new FileStream(filename, FileMode.Open, FileAccess.Read));

        var width = sr.ReadUInt64();
        var height = sr.ReadUInt64();

        if (width == 0 || height == 0)
        {
            ////_logger.Debug("Given image {pixelFile} has invalid dimentions\nwidth:{width}\theight:{height}", filename, width, height);

            throw new InvalidCaffException();
        }

        int widthInt = (int)width;
        int heightInt = (int)height;

        if ((ulong)widthInt < width || (ulong)heightInt < height)
        {
            throw new OverflowException();
        }

        using SKBitmap bitmap = new SKBitmap(widthInt, heightInt);

        var pixels = bitmap.Pixels;
        Span<byte> buffer = new Span<byte>(new byte[BUFFER_SIZE]);
        int count = 0;
        int passes = 0;
        while ((count = sr.Read(buffer)) != 0)
        {
            int delimiter = passes * PIXEL_COUNT;

            for (ushort i = 0; i < count; i += 3)
            {
                SKColor color = new SKColor(buffer[i], buffer[i + 1], buffer[i + 2]);
                int c = i / 3;

                int index = delimiter + c;

                pixels[index] = color;

            }

            ++passes;
        }

        bitmap.Pixels = pixels;

        using MemoryStream memStream = new MemoryStream();
        using SKManagedWStream wstream = new SKManagedWStream(memStream);

        bitmap.Encode(wstream, SKEncodedImageFormat.Jpeg, 5);

        byte[] data = memStream.ToArray();

        return data;
    }

    /// <summary>
    /// Helper method to generate arguments for native component.
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private string GetArgs(
        string filename)
    {
        ////_logger.Verbose("Method {method} called with args: {fileName}", nameof(GetArgs), filename);
        return $"{filename} --getThumbnail {_generatorDir}";
    }

    /// <summary>
    /// Creates a jpeg name for a file.
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    private static string GetFileName(
        string original)
    {
        var cleanName = original.Trim();
        var f = new FileInfo(cleanName);

        var x = f.Name.Replace(".caff", "");

        string filename = DateTime.UtcNow.Ticks.ToString() + x + ".jpeg";
        return filename;
    }

    /// <summary>
    /// Maximum buffersize to read in. Must be multiplicative of three (3)
    /// </summary>
    private const int BUFFER_SIZE = 1023;

    /// <summary>
    /// Pixel count per buffer
    /// </summary>
    private const int PIXEL_COUNT = BUFFER_SIZE / 3;
}

