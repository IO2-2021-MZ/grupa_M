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
using webApi.Enums;

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
        public int CreateNewOrder(NewOrder newOrder, int userId)
        {
            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || user.Role != (int)Role.customer)
                throw new UnathorisedException("Unathourized");

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

            order.CustomerId = userId;

            _context.Orders.Add(order);
            _context.SaveChanges();

            for (int i = 0; i < newOrder.PositionsId.Length; i++)
            {
                OrderDish orderDish = new OrderDish();
                orderDish.DishId = newOrder.PositionsId[i];
                orderDish.OrderId = order.Id;
                _context.OrderDishes.Add(orderDish);
                var updatedOrder = _context
                                    .Orders
                                    .FirstOrDefault(o => o.Id == order.Id);
                updatedOrder.OrderDishes.Add(orderDish);
            }
            _context.SaveChanges();

            return order.Id;
        }

        public OrderDTO GetOrderById(int? id, int userId)
        {
            // TODO(?): other exception for null user
            var order = _context
                        .Orders
                        .Include(o => o.Address)
                        .Include(o => o.DiscountCode)
                        .Include(o => o.Customer)
                        .Include(o => o.OrderDishes)
                        .Include(o => o.Restaurant)
                        .Include(o => o.Employee)
                        .FirstOrDefault(o => o.Id == id.Value);

            if (order is null)
                throw new NotFoundException("Resource not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var urs = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();

            if (user is null ||
                (user.Role == (int)Role.customer && order.CustomerId != user.Id) ||
                (user.Role == (int)Role.restaurateur && (urs is null || order.RestaurantId != urs.RestaurantId)) ||
                (user.Role == (int)Role.employee && order.RestaurantId != user.RestaurantId))
                throw new UnathorisedException("Unathourized");

            

            OrderDTO orderDTO;
            decimal originalPrice = 0;
            decimal finalPrice = 0;
            foreach (var orderDish in order.OrderDishes)
            {
                var dish = _context
                            .Dishes
                            .FirstOrDefault(d => d.Id == orderDish.DishId);
                originalPrice += dish.Price;
            }
            if (order.DiscountCode is null)
                finalPrice = originalPrice;
            else
                finalPrice = originalPrice * (100 - order.DiscountCode.Percent) * (decimal)0.01;

            switch (user.Role)
            {
                case (int)Role.admin:
                    {
                        orderDTO = _mapper.Map<OrderA>(order);
                        (orderDTO as OrderA).OriginalPrice = originalPrice;
                        (orderDTO as OrderA).FinalPrice = finalPrice;
                        break;
                    }
                case (int)Role.customer:
                    {
                        orderDTO = _mapper.Map<OrderC>(order);
                        (orderDTO as OrderC).Positions = new HashSet<PositionFromMenuDTO>();
                        foreach (var orderDish in order.OrderDishes)
                        {
                            var dish = _context
                                        .Dishes
                                        .FirstOrDefault(d => d.Id == orderDish.DishId);
                            var position = _mapper.Map<PositionFromMenuDTO>(dish);
                            (orderDTO as OrderC).Positions.Add(position);
                        }
                        (orderDTO as OrderC).OriginalPrice = originalPrice;
                        (orderDTO as OrderC).FinalPrice = finalPrice;
                        break;
                    }
                default: //Role.Restaurer and Role.Employee
                    {
                        orderDTO = _mapper.Map<OrderR>(order);
                        (orderDTO as OrderR).Positions = new HashSet<PositionFromMenuDTO>();
                        foreach (var orderDish in order.OrderDishes)
                        {
                            var dish = _context
                                        .Dishes
                                        .FirstOrDefault(d => d.Id == orderDish.DishId);
                            var position = _mapper.Map<PositionFromMenuDTO>(dish);
                            (orderDTO as OrderR).Positions.Add(position);
                        }
                        (orderDTO as OrderR).OriginalPrice = originalPrice;
                        (orderDTO as OrderR).FinalPrice = finalPrice;
                        break;
                    }
            }

            return orderDTO;
        }
        public List<OrderA> GetOrdersArchive()
        {
            var orders = _context
                        .Orders
                        .Include(o => o.Address)
                        .Include(o => o.DiscountCode)
                        .Include(o => o.Customer)
                        .Include(o => o.OrderDishes)
                        .Include(o => o.Restaurant)
                        .Include(o => o.Employee)
                        .ToList();

            if (orders is null)
                throw new NotFoundException("Resource not found");

            List<OrderA> orderDTOs = new List<OrderA>();
            decimal originalPrice = 0;
            decimal finalPrice = 0;
            for (int i = 0; i < orders.Count; i++)
            { 
                foreach(var orderDish in orders[i].OrderDishes)
                {
                    var dish = _context
                                .Dishes
                                .FirstOrDefault(d => d.Id == orderDish.DishId);
                    originalPrice += dish.Price;
                }
                if (orders[i].DiscountCode is null)
                    finalPrice = originalPrice;
                else
                    finalPrice = originalPrice * (100 - orders[i].DiscountCode.Percent) * (decimal)0.01;
                orderDTOs.Add(new OrderA());
                orderDTOs[i] = _mapper.Map<OrderA>(orders[i]);
                orderDTOs[i].OriginalPrice = originalPrice;
                orderDTOs[i].FinalPrice = finalPrice;
                originalPrice = 0;
                finalPrice = 0;
            }

            return orderDTOs;
        }
        public void AcceptOrder(int id, int userId)
        {
            var order = _context
                        .Orders
                        .FirstOrDefault(o => o.Id == id);

            if (order is null)
                throw new NotFoundException("Resource not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var urs = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();


            if (user is null ||
                user.Role == (int)Role.customer ||
                user.Role == (int)Role.admin ||
                (user.Role == (int)Role.restaurateur && (urs is null || order.RestaurantId != urs.RestaurantId)) ||
                user.Role == (int)Role.employee && order.RestaurantId != user.RestaurantId)
                throw new UnathorisedException("Unathourized");

            order.State = 1;
            order.EmployeeId = userId;
            _context.SaveChanges();
        }

        public void RealiseOrder(int id, int userId)
        {
            var order = _context
                        .Orders
                        .FirstOrDefault(o => o.Id == id);

            if (order is null)
                throw new NotFoundException("Resource not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();
            var urs = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();


            if (user is null ||
                user.Role == (int)Role.customer ||
                user.Role == (int)Role.admin ||
               (user.Role == (int)Role.restaurateur && (urs is null || order.RestaurantId != urs.RestaurantId)) ||
                user.Role == (int)Role.employee && order.RestaurantId != user.RestaurantId)
                throw new UnathorisedException("Unathourized");

            order.State = 2;
            order.EmployeeId = userId;
            _context.SaveChanges();
        }

        public void RefuseOrder(int id, int userId)
        {
            var order = _context
                        .Orders
                        .FirstOrDefault(o => o.Id == id);

            if (order is null)
                throw new NotFoundException("Resource not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var urs = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();


            if (user is null ||
                user.Role == (int)Role.customer ||
                user.Role == (int)Role.admin ||
              (user.Role == (int)Role.restaurateur && (urs is null || order.RestaurantId != urs.RestaurantId)) ||
                user.Role == (int)Role.employee && order.RestaurantId != user.RestaurantId)
                throw new UnathorisedException("Unathourized");

            order.State = 3;
            order.EmployeeId = userId;
            _context.SaveChanges();

        }
    }
}
