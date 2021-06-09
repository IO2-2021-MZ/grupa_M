using System.Collections.Generic;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;

namespace webApi.Services
{
    public interface IDiscountCodeService
    {
        public DiscountCodeDTO GetDiscountCodeByCode(string? code, int userId);
        int CreateNewDiscountCode(NewDiscountCode newDiscountCode, int userId, int? restaurantId);
        bool DeleteDiscountCode(int id, int userId);
        IEnumerable<DiscountCodeDTO> GetAllDiscountCodes(int userId);
    }
}