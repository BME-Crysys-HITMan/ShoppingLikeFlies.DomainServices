using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Service
{
    class PaymentService : IPaymentService
    {
        private IDataService dataService;
        public PaymentService(IDataService dataService)
        {
            this.dataService = dataService;
        }
        async Task<bool> BuyItemAsync(int userId, int price, int caffId)
        {
            if (price < 0)
                return false;
            CaffDTO caffDTO = await this.dataService.AsyncGetCaff(caffId);
            if (caffDTO == null)
                return false;
            return true;
        }
    }
}
