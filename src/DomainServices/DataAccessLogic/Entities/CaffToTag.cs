using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DataAccessLogic.Entities
{
    public class CaffToTag
    {
        public int CaffId { get; set; }
        public int CaffTagId { get; set; }
        public virtual Caff Caff { get; set; }
        public virtual CaffTag CaffTag { get; set; }
    }
}
