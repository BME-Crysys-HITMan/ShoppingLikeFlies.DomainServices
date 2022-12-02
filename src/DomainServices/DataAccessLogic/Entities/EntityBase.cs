using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DataAccessLogic.Entities
{
    public abstract class EntityBase<T> where T : class, new()
    {
        public int Id { get; set; }

        public abstract void IncludeAll(IQueryable<T> queryable);
    }
}
