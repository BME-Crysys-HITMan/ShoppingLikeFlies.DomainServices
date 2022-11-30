using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.DTOs
{
    public class CaffSearchDTO
    {
        public string Creator { get; set; }
        public string Caption { get; set; }
        public List<string> Tags { get; set; }
    }
}
