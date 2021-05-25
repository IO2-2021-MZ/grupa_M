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

        List<OrderA> GetOrdersArchive();
        int CreateNewOrder(NewOrder newOrder, int userId);
        void RefuseOrder(int id, int userId);
        void AcceptOrder(int id, int userId);
        void RealiseOrder(int id, int userId);
    }
}