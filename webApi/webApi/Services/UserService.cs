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
            return _context.Users.SingleOrDefault(user => user.Id == id);
           
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
            if (id == null) throw new BadRequestException("DeleteUser id is null");
            var user = _context.Users.SingleOrDefault(user => user.Id == id);
            if (user == null) throw new BadRequestException("User not Exist");
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public IEnumerable<Order> GetAllUserOrders(int? id)
        {
            if (id == null) throw new BadRequestException("GetAllUserOrders id is null");
            var orders = _context.Orders.Where(order => order.CustomerId == id).ToList();
            return orders;
        }
        public IEnumerable<Complaint> GetAllUserComplaint(int? id)
        {
            if (id == null) throw new BadRequestException("GetAllUserOrders id is null");
            var complaints = _context.Complaints.Where(complaint => complaint.CustomerId == id).ToList();
            return complaints;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();

        }
    }
}
