﻿using CliWrap;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Options;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace ShoppingLikeFiles.DomainServices.Core.Internal;

internal class DefaultCaffValidator : ICaffValidator
{
    private readonly string _validator;
    private const string validateArgument = "--validate";

    public DefaultCaffValidator(IOptions<CaffValidatorOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrEmpty(options.Value.Validator))
        {
            throw new ArgumentNullException(nameof(options));
        }

        _validator = options.Value.Validator;
    }

    internal DefaultCaffValidator(string validator)
    {
        if (string.IsNullOrEmpty(validator))
        {
            throw new ArgumentNullException(nameof(validator));
        }

        _validator = validator;
    }

    public bool ValidateFile(string fileName)
    {
        string cleanFileName = fileName.Trim();
        if (string.IsNullOrEmpty(cleanFileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }
        var arguments = GetArguments(cleanFileName);

        try
        {
            using Process process = new();
            process.StartInfo.FileName = _validator;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            bool result = process.Start();

            if (!result)
            {
                return result;
            }
            StreamReader reader = process.StandardOutput;
            var line = reader.ReadLine();

            process.WaitForExit();

            if (line == "1")
            {
                return true;
            }
        }
        catch (Win32Exception ex)
        {
            //TODO logging
        }

        return false;
    }

    public Task<bool> ValidateFileAsync(string fileName)
    {
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

    private async Task<bool> ValidateFileInternalAsync(string filename)
    {
        var output = new StringBuilder();
        try
        {
            await Cli.Wrap(_validator)
            .WithArguments(GetArguments(filename))
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(output))
            .ExecuteAsync();

            var response = output.ToString().Trim();

            if (response == "1")
            {
                return true;
            }
        }
        catch (Win32Exception ex)
        {
            //TODO logging
        }

        return false;
    }

    private string GetArguments(string filename) => $"{filename} {validateArgument}";
}

