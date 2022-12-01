using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Options
{
    public class UploadServiceOptions
    {
        public string DirectoryPath { get; set; }
        public bool ShouldUploadToAzure { get; set; }
    }
}
