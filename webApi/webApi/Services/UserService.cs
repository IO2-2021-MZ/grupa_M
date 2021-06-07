using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.AuthenticateDTO;
using BC = BCrypt.Net.BCrypt;
using webApi.Exceptions;
using webApi.Models;
using webApi.DataTransferObjects.UserDTO;
using webApi.Enums;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.RestaurantDTO;

namespace webApi.Services
{

    public class UserService : IUserService
    {
        IO2_RestaurantsContext _context;
        IMapper _mapper;
        public UserService(IO2_RestaurantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public User GetUserWithId(int? id)
        {
            if (id == null) throw new BadRequestException(" GetUser id is null");
           return _context.Users
                .Include(x=> x.Restaurant)
                .Include(x=>x.Address)
                .Include(x=>x.UserRests)
                .ThenInclude(y => y.Restaurant)
                .SingleOrDefault(user => user.Id == id);
        
        }
        public RegisterResponse CreateNewUser(RegisterRequest newUser)
        {
            var exsUser = _context.Users.SingleOrDefault(usr => usr.Email == newUser.Email);
            if (exsUser != null) throw new BadRequestException("User already exists");
            var address = _context.Addresses.SingleOrDefault(
                addr => addr.City == newUser.City && addr.PostCode == newUser.PostCode && 
                addr.Street == newUser.Street);
            if(address ==  null)
            {
                address = new Address { Street = newUser.Street, PostCode = newUser.PostCode, City = newUser.City };
            }
            
            _context.Addresses.Add(address);
            _context.SaveChanges();
            var user = _mapper.Map<User>(newUser);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            user.CreationDate = DateTime.Now;
            user.AddressId = address.Id;
            _context.Users.Add(user);
            _context.SaveChanges();

            return new RegisterResponse { Id = user.Id };


        }
        public void DeleteUser(int? id)
        {
            if (id is null) throw new BadRequestException("id cannot be null");
            var user = _context.Users.SingleOrDefault(user => user.Id == id);
            if (user == null) throw new NotFoundException("User does not Exist");
            
            var reviews = _context.Reviews.Where(r => r.CustomerId == user.Id);
            _context.Reviews.RemoveRange(reviews);
            _context.SaveChanges();

            var complaints = _context.Complaints.Where(r => r.CustomerId == user.Id || r.EmployeeId == user.Id);
            _context.Complaints.RemoveRange(complaints);
            _context.SaveChanges();

            var orders2 = _context.OrderDishes.Include(o => o.Order).Where(r => r.Order.CustomerId == user.Id || r.Order.EmployeeId == user.Id);
            _context.OrderDishes.RemoveRange(orders2);
            _context.SaveChanges();

            var orders = _context.Orders.Where(r => r.CustomerId == user.Id);
            _context.Complaints.RemoveRange(complaints);
            _context.SaveChanges()

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public IEnumerable<OrderC> GetAllUserOrders(int id)
        {
            var orders = _context.Orders
                .Include(o => o.Address)
                .Include(o => o.DiscountCode)
                .Include(o => o.Customer)
                .Include(o => o.OrderDishes)
                .Include(o => o.Restaurant)
                .Include(o => o.Employee)
                .Where(order => order.CustomerId == id).ToList();
            if(orders is null)
                throw new NotFoundException("Resource not found");
            List<OrderC> orderDTOs = new List<OrderC>();
            decimal originalPrice = 0;
            decimal finalPrice = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                foreach (var orderDish in orders[i].OrderDishes)
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
                orderDTOs.Add(new OrderC());
                orderDTOs[i] = _mapper.Map<OrderC>(orders[i]);
                orderDTOs[i].Positions = new HashSet<PositionFromMenuDTO>();
                foreach (var orderDish in orders[i].OrderDishes)
                {
                    var dish = _context
                                .Dishes
                                .FirstOrDefault(d => d.Id == orderDish.DishId);
                    var position = _mapper.Map<PositionFromMenuDTO>(dish);
                    orderDTOs[i].Positions.Add(position);
                }
                orderDTOs[i].OriginalPrice = originalPrice;
                orderDTOs[i].FinalPrice = finalPrice;
                originalPrice = 0;
                finalPrice = 0;
            }
            return orderDTOs;
        }
        public IEnumerable<Complaint> GetAllUserComplaint(int? id)
        {
            if (id == null) throw new BadRequestException("GetAllUserOrders id is null");
            var complaints = _context.Complaints.Where(complaint => complaint.CustomerId == id).ToList();
            return complaints;
        }
        public IEnumerable<User> GetAllUsers(int role)
        {
            return _context.Users.Where(x=> x.Role== role);

        }

        public User CreateNewEmployee(NewEmployee value)
        {
            var user = _mapper.Map<User>(value);
            user.Role = value.isRestaurateur ? (int)Role.restaurateur : (int)Role.employee;
            user.RestaurantId = _context.Restaurants.Any(x => x.Id == value.restaurantId)  ? value.restaurantId : null;
            user.PasswordHash = "$2a$11$yJdjz6naBO1kL3O0dc1dke4BOJSuXUm8yNmnkocFRIb/GYCPSehyK";
            user.CreationDate = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User CreateNewAdmin(NewAdministrator value)
        {
            var user = _mapper.Map<User>(value);
            user.Role = (int)Role.admin;
            user.PasswordHash = "$2a$11$yJdjz6naBO1kL3O0dc1dke4BOJSuXUm8yNmnkocFRIb/GYCPSehyK";
            user.CreationDate = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;

        }
        public User CreateNewCustomer(NewCustomer value)
        {
            var address = _mapper.Map<Address>(value.address);
            var user = _mapper.Map<User>(value);
            user.Address = address;
            user.Role = (int)Role.customer;
            user.PasswordHash = "$2a$11$yJdjz6naBO1kL3O0dc1dke4BOJSuXUm8yNmnkocFRIb/GYCPSehyK";
            user.CreationDate = DateTime.Now;
            _context.Users.Add(user);
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return user;
        }

    }
}
