using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Entities
{
    public class CaffTag
    {
        public CaffTag()
        {
            CaffToTags= new HashSet<CaffToTag>();
        }
        public int Id { get; set; }
        public string Tag { get; set; }
        public ICollection<CaffToTag> CaffToTags { get; set; }
    }
}
