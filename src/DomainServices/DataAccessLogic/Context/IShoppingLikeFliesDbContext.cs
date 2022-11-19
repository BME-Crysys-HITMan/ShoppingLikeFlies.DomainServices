using DataAccessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Context
{
    public interface IShoppingLikeFliesDbContext
    {
        DbSet<Caff> Caff { get; set; }
        DbSet<CaffTag> CaffTag { get; set; }
        DbSet<CaffToTag> CaffToTag { get; set; }
    }
}
