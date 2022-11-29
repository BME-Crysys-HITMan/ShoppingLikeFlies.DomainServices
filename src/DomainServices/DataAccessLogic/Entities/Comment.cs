using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Entities
{
    class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CaffId { get; set; }
        public virtual Caff Caff { get; set; }
        public int UserId { get; set; }
    }
}
