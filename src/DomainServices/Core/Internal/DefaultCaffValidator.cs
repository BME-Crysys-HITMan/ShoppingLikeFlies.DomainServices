using CliWrap;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Options;

namespace ShoppingLikeFiles.DomainServices.Core.Internal;

internal class DefaultCaffValidator : ICaffValidator
{
    private readonly string _validator;

    public DefaultCaffValidator(IOptions<CaffValidatorOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _validator = options.Value.Validator;
    }

    internal DefaultCaffValidator(string validator)
    {
        _validator = validator;
    }

    public bool ValidateFile(string fileName)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ValidateFileAsync(string fileName)
    {
        string cleanFileName = fileName.Trim();
        if (string.IsNullOrEmpty(cleanFileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        Stream output = new MemoryStream();
        await Cli.Wrap(_validator)
            .WithArguments(fileName)
            .WithStandardOutputPipe(PipeTarget.ToStream(output, true))
            .ExecuteAsync();

        var response = output.ReadByte();

        output.Close();

        if (response == 1)
        {
            return true;
        }

        return false;
    }
}

