using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Options
{
    internal sealed class UploadServiceOptionsSetup : IConfigureOptions<UploadServiceOptions>
    {
        public void Configure(UploadServiceOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if(options.DirectoryPath == null)
            {
                options.DirectoryPath = ".\\file_directory";
            }
        }
    }
}
