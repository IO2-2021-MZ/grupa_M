using System.Collections.Generic;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
using webApi.Models;

namespace webApi.Services
{
    public interface IDiscountCodeService
    {
        public DiscountCode GetDiscountCodeById(int? id);
        int CreateNewDiscountCode(NewDiscountCode newDiscountCode);
        void DeleteDiscountCode(int id);
        IEnumerable<DiscountCode> GetAllDiscountCodes();
    }
}