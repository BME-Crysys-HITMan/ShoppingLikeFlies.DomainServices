using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Entities
{
    public class Caption : EntityBase<Caption>
    {
        public string Text { get; set; }
        public int CaffId { get; set; }
        public virtual Caff Caff { get; set; }
        public override void IncludeAll(IQueryable<Caption> queryable)
        {
            queryable.Include(x => x.Caff);
        }
    }
}
