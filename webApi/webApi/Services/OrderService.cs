using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;

namespace webApi.Services
{
    public class OrderService : IOrderService
    {
        private Models.IO2_RestaurantsContext _context;
        public OrderService(Models.IO2_RestaurantsContext context)
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

        public OrderDTO GetOrderById(int? id)
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
