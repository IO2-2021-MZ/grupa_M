using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.UserDTO;
using webApi.Exceptions;
using webApi.Models;

namespace webApi.Services
{

    public class UserService : IUserService
    {
        Models.IO2_RestaurantsContext _context;
        public UserService(Models.IO2_RestaurantsContext context)
        {
            _context = context;
        }
        public User GetUserWithId(int? id)
        {
            if (id == null) throw new BadRequestException(" GetUser id is null");
            return _context.Users.SingleOrDefault(user => user.Id == id);
           
        }
        public int CreateNewUser(User newUser)
        {
             _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser.Id;
        }
        public void DeleteUser(int? id)
        {
            if (id == null) throw new BadRequestException("DeleteUser id is null");
            var user = _context.Users.SingleOrDefault(user => user.Id == id);
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
