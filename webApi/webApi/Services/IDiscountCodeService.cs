using System.Collections.Generic;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;

namespace webApi.Services
{
    public interface IDiscountCodeService
    {
        public TransferDiscountCode GetDiscountCodeById(int? id);
        int CreateNewDiscountCode(NewDiscountCode newDiscountCode);
        bool DeleteDiscountCode(int id);
        IEnumerable<DiscountCode> GetAllDiscountCodes();
    }
}