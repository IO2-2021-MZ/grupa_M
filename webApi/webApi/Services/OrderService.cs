using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
using webApi.Models;

namespace webApi.Services
{
    public class OrderService : IOrderService
    {
        private IO2_RestaurantsContext _context;
        public OrderService(IO2_RestaurantsContext context)
        {
            _context = context;
        }

        public void AcceptOrder(int id)
        {
            throw new NotImplementedException();
        }

        public int CreateNewOrder(NewOrder newOrder)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int? id)
        {
            throw new NotImplementedException();
        }

        public void RealiseOrder(int id)
        {
            throw new NotImplementedException();
        }

        public void RefuseOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
