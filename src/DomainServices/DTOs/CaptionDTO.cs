using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.DTOs
{
    public class CaptionDTO
    {
        public int Id { get; set; }
        public int CaffId { get; set; }
        public CaffDTO Caff { get; set; }
        public string Text { get; set; }
    }
}
