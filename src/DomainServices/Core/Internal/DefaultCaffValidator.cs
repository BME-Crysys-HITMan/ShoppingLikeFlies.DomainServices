using ShoppingLikeFiles.DomainServices.Exceptions;
using ShoppingLikeFiles.DomainServices.Model;

namespace ShoppingLikeFiles.DomainServices.Core.Internal;

internal class DefaultCaffValidator : ICaffValidator
{
    private readonly INativeCommunicator _communicator;
    private readonly ILogger _logger;
    private const string validateArgument = "--validate";


    public DefaultCaffValidator(INativeCommunicator communicator, ILogger logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        _logger = logger.ForContext<DefaultCaffValidator>();
        _communicator = communicator ?? throw new ArgumentNullException(nameof(communicator));
    }

    public CaffCredit? ValidateFile(string fileName)
    {
        _logger.Verbose("Called {method} with {fileName}", nameof(ValidateFile), fileName);
        string cleanFileName = fileName.Trim();
        if (string.IsNullOrEmpty(cleanFileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }
        var arguments = GetArguments(cleanFileName);

        var response = _communicator.Communicate(arguments);
        _logger.Verbose("Native returned with {response}", response);
        if (!string.IsNullOrEmpty(response))
        {

            if (response.StartsWith("0"))
            {
                _logger.Debug("Got an invalid file here.");
                return null;
            }
            return GetCredit(response);
        }

        return null;
    }

    public Task<CaffCredit?> ValidateFileAsync(string fileName)
    {
        _logger.Verbose("Called {method} with {fileName}", nameof(ValidateFileAsync), fileName);
        string cleanFileName = fileName.Trim();
        if (string.IsNullOrEmpty(cleanFileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        /**
         * Fix for codesmell
         */
        return ValidateFileInternalAsync(cleanFileName);
    }

    private async Task<CaffCredit?> ValidateFileInternalAsync(string filename)
    {
        _logger.Verbose("Called {method} with {fileName}", nameof(ValidateFileInternalAsync), filename);

        var response = await _communicator.CommunicateAsync(GetArguments(filename));
        _logger.Verbose("Native returned with {response}", response);

        if (response.StartsWith("0"))
        {
            _logger.Debug("Got an invalid file here.");
            return null;
        }
        if (!string.IsNullOrEmpty(response))
        {
            return GetCredit(response);
        }

        return null;
    }

    private string GetArguments(string filename) => $"{filename} {validateArgument}";

    private CaffCredit GetCredit(string response)
    {
        _logger.Verbose("Method {method} called with args: {}", nameof(GetCredit), response);

        if (string.IsNullOrEmpty(response))
        {
            throw new ArgumentNullException($"{nameof(response)}");
        }

        var lines = response.Split("\r\n");

        string[] date = lines[1].Split(":");

        if (date.Length != 5)
            throw new InvalidCaffException();

        ushort y = ushort.Parse(date[0]);
        byte m = byte.Parse(date[1]);
        byte d = byte.Parse(date[2]);
        byte h = byte.Parse(date[3]);
        byte mm = byte.Parse(date[4]);

        string creator = lines[2];

        var tags = lines[3].Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();

        CaffCredit credit = new CaffCredit(y, m, d, h, mm, creator);

        credit.Tags = tags;

        return credit;
    }
}

