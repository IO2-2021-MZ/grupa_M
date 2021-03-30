using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.Models;

namespace webApi.Services
{
    public class OrderService : IOrderService
    {
        private IO2_RestaurantsContext _context;
        private readonly IMapper _mapper;
        public OrderService(Models.IO2_RestaurantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AcceptOrder(int id)
        {
            throw new NotImplementedException();
        }

        public int CreateNewOrder(NewOrder newOrder)
        {
            var no = _mapper.Map<Order>(newOrder);
            _context.Orders.Add(no);
            _context.SaveChanges();
            return no.Id;
        }

        public OrderDTO GetOrderById(int? id)
        {
            if (id == null)
                return null;
            return _mapper.Map<OrderDTO>(_context.Orders.FirstOrDefault(o => o.Id == id.Value));
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
