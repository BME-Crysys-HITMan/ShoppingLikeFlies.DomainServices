using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Service
{
    internal class CaffService : ICaffService
    {
        public string Ping()
        {
            return "pong";
        }
    }
}
