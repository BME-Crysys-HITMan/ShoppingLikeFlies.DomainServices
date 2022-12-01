using Microsoft.Extensions.Options;

namespace ShoppingLikeFiles.DomainServices.Options
{
    internal sealed class CaffValidatorOptionsSetup : IConfigureOptions<CaffValidatorOptions>
    {
        public void Configure(CaffValidatorOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.Validator == null)
            {
                options.Validator = "CAFF_Processor";
            }

            if (string.IsNullOrEmpty(options.GeneratorDir))
            {
                string cwd = Directory.GetCurrentDirectory();

                Directory.CreateDirectory(cwd + "/generated");

                var dirs = Directory.GetDirectories(cwd, "generated");

                options.GeneratorDir = dirs.First();
            }
        }
    }
}
