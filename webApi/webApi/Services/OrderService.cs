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
using webApi.Exceptions;
using Microsoft.EntityFrameworkCore;

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
            var order = _context
                        .Orders
                        .FirstOrDefault(o => o.Id == id);
            if (order is null)
                throw new NotFoundException("Resource not found");
            order.State = 1;
            _context.SaveChanges();
        }

        public int CreateNewOrder(NewOrder newOrder)
        {
            //TODO(?): handling wrong IDs
            if (newOrder is null)
                throw new BadRequestException("Bad request");
            var order = _mapper.Map<Order>(newOrder);
            var address = _mapper.Map<Address>(newOrder.Address);
            Address existingAddress = _context
                                        .Addresses
                                        .FirstOrDefault(a => a.City == address.City && a.PostCode == address.PostCode && a.Street == address.Street);
            if (existingAddress is null)
            {
                _context.Addresses.Add(address);
                _context.SaveChanges();
                order.AddressId = address.Id;
            }
            else
            {
                order.AddressId = existingAddress.Id;
            }
            order.State = 0;
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public OrderDTO GetOrderById(int? id)
        {
            var order = _context
                        .Orders
                        .Include(o => o.Address)
                        .FirstOrDefault(o => o.Id == id.Value);

            if (order is null)
                throw new NotFoundException("Resource not found");

            return _mapper.Map<OrderDTO>(order);
        }

        public void RealiseOrder(int id)
        {
            var order = _context
                        .Orders
                        .FirstOrDefault(o => o.Id == id);
            if (order is null)
                throw new NotFoundException("Resource not found");
            order.State = 2;
            _context.SaveChanges();
        }

        public void RefuseOrder(int id)
        {
            var order = _context
                        .Orders
                        .FirstOrDefault(o => o.Id == id);
            if (order is null)
                throw new NotFoundException("Resource not found");
            order.State = 3;
            _context.SaveChanges();

        }
    }
}
