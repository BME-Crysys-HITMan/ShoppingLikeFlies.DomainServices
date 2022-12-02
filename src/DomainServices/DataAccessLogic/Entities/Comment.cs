using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DataAccessLogic.Entities
{
    public class Comment : EntityBase<Comment>
    {
        public string Text { get; set; }
        public int CaffId { get; set; }
        public virtual Caff Caff { get; set; }
        public int UserId { get; set; }
        public override void IncludeAll(IQueryable<Comment> queryable)
        {
            queryable.Include(x => x.Caff);
        }
    }
}
