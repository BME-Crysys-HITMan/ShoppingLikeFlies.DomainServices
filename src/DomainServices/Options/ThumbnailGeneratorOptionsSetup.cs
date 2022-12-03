using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Options
{
    internal sealed class ThumbnailGeneratorOptionsSetup : IConfigureOptions<ThumbnailGeneratorOptions>
    {
        public void Configure(ThumbnailGeneratorOptions options)
        {
            if(options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrEmpty(options.GeneratorDir))
            {
                var path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "generator";
                Directory.CreateDirectory(path);
                options.GeneratorDir = path;
            }
        }
    }
}
