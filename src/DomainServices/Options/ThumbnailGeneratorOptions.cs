using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Options
{
    public class ThumbnailGeneratorOptions
    {
        /// <summary>
        /// Secifies a folder that the CAFF_Processor should put its outputs at.
        /// </summary>
        public string GeneratorDir { get; set; } = string.Empty;
    }
}
