using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Entities
{
    public class CaffTag : EntityBase<CaffTag>
    {
        public CaffTag()
        {
            CaffToTags= new HashSet<CaffToTag>();
        }
        public string Tag { get; set; }
        public ICollection<CaffToTag> CaffToTags { get; set; }

        public override void IncludeAll(IQueryable<CaffTag> queryable)
        {
            queryable.Include(x => x.CaffToTags).ThenInclude(x => x.Caff.Comments);
        }
    }
}
