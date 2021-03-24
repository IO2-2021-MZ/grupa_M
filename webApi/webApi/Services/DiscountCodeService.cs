using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.DiscountCode;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;

namespace webApi.Services
{
    public class DiscountCodeService : IDiscountCodeService
    {
        public int CreateNewDiscountCode(NewDiscountCode newDiscountCode)
        {
            throw new NotImplementedException();
        }

        public void DeleteDiscountCode(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransferDiscountCode> GetAllDiscountCodes()
        {
            throw new NotImplementedException();
        }

        public TransferDiscountCode GetDiscountCodeById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
