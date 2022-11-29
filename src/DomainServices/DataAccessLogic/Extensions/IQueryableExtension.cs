using DataAccessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : EntityBase<T>, new()
        {
            new T().IncludeAll(queryable);
            return queryable;
        }
    }
}
