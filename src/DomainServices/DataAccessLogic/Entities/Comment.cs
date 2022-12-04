using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DataAccessLogic.Entities
{
    public class Comment : EntityBase
    {
        public string Text { get; set; } = string.Empty;
        public int CaffId { get; set; }
        public Caff? Caff { get; set; }
        public Guid UserId { get; set; }
    }
}
