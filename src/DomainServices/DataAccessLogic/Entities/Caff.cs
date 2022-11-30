using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Entities
{
    public class Caff : EntityBase<Caff>
    {
        public Caff()
        {
            Tags = new HashSet<CaffToTag>();
            Comments = new HashSet<Comment>();
        }
        public string FilePath { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Creator { get; set; }
        public ICollection<CaffToTag> Tags { get; set; }
        public string ThumbnailPath { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public override void IncludeAll(IQueryable<Caff> queryable)
        {
            queryable.Include(x => x.Tags).ThenInclude(x => x.CaffTag);
            queryable.Include(x => x.Comments);
        }
    }
}
