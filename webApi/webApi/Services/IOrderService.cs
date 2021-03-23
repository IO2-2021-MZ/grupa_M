using System.Collections.Generic;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;

namespace webApi.Services
{
    public interface IOrderService
    {
        Order GetOrderById(int? id);
        int CreateNewOrder(NewOrder newOrder);
        void RefuseOrder(int id);
        void AcceptOrder(int id);
        void RealiseOrder(int id);
    }
}