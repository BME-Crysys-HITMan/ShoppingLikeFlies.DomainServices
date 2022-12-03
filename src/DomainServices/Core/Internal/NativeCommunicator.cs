using CliWrap;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Options;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace ShoppingLikeFiles.DomainServices.Core.Internal
{
    internal class NativeCommunicator : INativeCommunicator
    {
        private readonly string _processorPath;
        //private readonly ILogger ////_logger;

        public NativeCommunicator(IOptions<CaffValidatorOptions> options)//, ILogger logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrEmpty(options.Value.Validator))
            {
                throw new ArgumentNullException(nameof(options));
            }

            _processorPath = options.Value.Validator;
            //////_logger = logger.ForContext<NativeCommunicator>();
        }

        public string? Communicate(string? args)
        {
            ////_logger.Verbose("Called {method} with args: {args}", nameof(Communicate), args);
            try
            {
                using Process process = new();
                process.StartInfo.FileName = _processorPath;
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                bool result = process.Start();

                if (!result)
                {
                    ////_logger.Warning("Could not start process");
                    return null;
                }

                var output = process.StandardOutput.ReadToEnd().Trim();
                var error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                ////_logger.Verbose("Native call stdout: {stdout}", output);
                ////_logger.Verbose("Native call stderr: {stderr}", error);

                if (error.Trim() != string.Empty)
                {
                    ////_logger.Debug("Native call returned error:\n{error}", error);
                }

                return output;
            }
            catch (Win32Exception ex)
            {
                ////_logger.Warning("Failed to validate file.");
                ////_logger.Debug(ex, "Exception during file validation, returning null...");
            }

            return null;
        }

        public async Task<string?> CommunicateAsync(string? args)
        {
            ////_logger.Verbose("Called {method} with args: {args}", nameof(Communicate), args);
            try
            {
                string arguments = args is null ? "" : args;
                var output = new StringBuilder();
                var error = new StringBuilder();
                await Cli.Wrap(_processorPath)
                .WithArguments(arguments)
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(output))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(error))
                .ExecuteAsync();

                ////_logger.Verbose("Native call stdout: {stdout}", output.ToString());
                ////_logger.Verbose("Native call stderr: {stderr}", error.ToString());

                if (error.ToString().Trim() != "")
                {
                    ////_logger.Debug("Native call returned with error:\n{error}", error.ToString());
                    return null;
                }

                return output.ToString().Trim();
            }
            catch (Win32Exception ex)
            {
                ////_logger.Error("Failed to validate file.");
                ////_logger.Debug(ex, "Exception during file validation, returning null...");
            }

            return null;
        }
    }
}
