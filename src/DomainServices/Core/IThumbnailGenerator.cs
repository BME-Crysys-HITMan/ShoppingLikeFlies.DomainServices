using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Core
{
    internal interface IThumbnailGenerator
    {
        byte[][] GenerateThumbnail();
    }
}
