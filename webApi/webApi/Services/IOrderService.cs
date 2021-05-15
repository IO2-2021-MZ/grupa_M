using System.Collections.Generic;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;

namespace webApi.Services
{
    public interface IOrderService
    {
        OrderDTO GetOrderById(int? id, int userId);
        int CreateNewOrder(NewOrder newOrder, int userId);
        void RefuseOrder(int id);
        void AcceptOrder(int id);
        void RealiseOrder(int id);
    }
}