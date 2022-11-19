using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Entities
{
    public class Caff
    {
        public Caff()
        {
            Tags = new HashSet<CaffToTag>();
        }
        public int Id { get; set; }
        public string FilePath { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Creator { get; set; }
        public ICollection<CaffToTag> Tags { get; set; }
        public string ThumbnailPath { get; set; }
    }
}
