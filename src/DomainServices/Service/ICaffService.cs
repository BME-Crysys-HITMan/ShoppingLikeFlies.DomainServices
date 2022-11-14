using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Service
{
    public interface ICaffService
    {
        /// <summary>
        /// A tester method the must return "pong"
        /// </summary>
        /// <returns> a simple "pong" string </returns>
        string Ping();
    }
}
