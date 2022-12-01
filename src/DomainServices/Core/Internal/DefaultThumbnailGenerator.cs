using CliWrap;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Options;
using SkiaSharp;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace ShoppingLikeFiles.DomainServices.Core.Internal;

internal class DefaultThumbnailGenerator : IThumbnailGenerator
{
    private readonly string _generatorDir;
    private readonly string _validator;
    private readonly IUploadService _uploadService;
    public DefaultThumbnailGenerator(IOptions<CaffValidatorOptions> options, IUploadService uploadService)
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

        if (string.IsNullOrEmpty(x.Validator) || string.IsNullOrEmpty(x.GeneratorDir))
        {
            throw new ArgumentNullException(nameof(options));
        }

        _generatorDir = x.GeneratorDir;
        _validator = x.Validator;
        _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
    }

    internal DefaultThumbnailGenerator(string validator, string generatorDir, IUploadService uploadService)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _generatorDir = generatorDir ?? throw new ArgumentNullException(nameof(generatorDir));
        _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
    }

    public string? GenerateThumbnail(string caffFile)
    {
        var cleanName = caffFile.Trim();
        if (string.IsNullOrEmpty(cleanName))
        {
            throw new ArgumentNullException(nameof(caffFile));
        }

        var pixelFile = GeneratePixelsFile(cleanName);

        if (string.IsNullOrEmpty(pixelFile))
        {
            return null;
        }


        return ProcessPixel(pixelFile);
    }

    public async Task<string?> GenerateThumbnailAsync(string caffFile)
    {
        if (string.IsNullOrEmpty(caffFile))
        {
            throw new ArgumentNullException(nameof(caffFile));
        }

        var pixelFile = await GeneratePixelsFileAsync(caffFile);

        if (string.IsNullOrEmpty(pixelFile))
        {
            return null;
        }

        return await ProcessPixelAsync(pixelFile);
    }

    private string? GeneratePixelsFile(string caffFile)
    {
        try
        {
            ProcessStartInfo info = new()
            {
                FileName = _validator,
                Arguments = GetArgs(caffFile),
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };
            using Process process = new();
            process.StartInfo = info;
            bool started = process.Start();
            if (!started) //TODO Error log
                return null;

            var error = process.StandardError.ReadToEnd();
            var fileName = process.StandardOutput.ReadLine();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(error.Trim()))
            {
                //TODO log error
                return null;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                //TODO log error
                return null;
            }

            return fileName.Trim();
        }
        catch (Win32Exception ex)
        {
            //TODO log error
            return null;
        }
    }

    private async Task<string?> GeneratePixelsFileAsync(string caffFile)
    {
        var outputs = new StringBuilder();
        var errors = new StringBuilder();
        await Cli.Wrap(_validator)
            .WithArguments(GetArgs(caffFile))
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(outputs))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(errors))
            .ExecuteAsync();

        var error = errors.ToString().Trim();

        if (error.Length > 0)
        {
            //Log error

            return null;
        }

        var file = outputs.ToString().Trim();

        return file;
    }

    private async Task<string?> ProcessPixelAsync(string pixelFile)
    {
        if (string.IsNullOrEmpty(pixelFile))
        {
            throw new ArgumentNullException(nameof(pixelFile));
        }

        var data = GenerateJpeg(pixelFile);

        var filename = GetFileName(pixelFile);

        return await _uploadService.UploadFileAsync(data); //TODO _uploadService.UploadFileAsync(data, filename);
    }

    private string? ProcessPixel(string pixelFile)
    {
        if (string.IsNullOrEmpty(pixelFile.Trim()))
        {
            throw new ArgumentNullException(nameof(pixelFile));
        }

        var data = GenerateJpeg(pixelFile);

        var fileName = GetFileName(pixelFile);

        return _uploadService.UploadFile(data); //TODO _uploadService.UploadFile(data, fileName);
    }

    private byte[] GenerateJpeg(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        using BinaryReader sr = new BinaryReader(new FileStream(filename, FileMode.Open, FileAccess.Read));

        var width = sr.ReadUInt64();
        var height = sr.ReadUInt64();

        if (width == 0 || height == 0)
        {
            return null;
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

    private string GetArgs(string filename)
    {
        return $"{filename} --getThumbnail {_generatorDir}";
    }

    private static string GetFileName(string original)
    {
        var cleanName = original.Trim();
        var f = new FileInfo(cleanName);

        var x = f.Name.Replace(".caff", "");

        string filename = DateTime.UtcNow.Ticks.ToString() + x + ".jpeg";
        return "";
    }

    private const int BUFFER_SIZE = 1023;
    private const int PIXEL_COUNT = BUFFER_SIZE / 3;
}

