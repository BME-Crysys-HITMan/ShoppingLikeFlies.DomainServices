using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if(options.Validator == null)
            {
                options.Validator = "my_validator";
            }
        }
    }
}
