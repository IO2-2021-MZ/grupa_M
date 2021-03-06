using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.UserDTO;
using webApi.Models;

namespace webApi.Services
{
    public interface IUserService
    {
        public User GetUserWithId(int? id);
        public int CreateNewUser(User newUser);
        public void DeleteUser(int? id );
        public IEnumerable<Order> GetAllUserOrders(int? id);
        public IEnumerable<Complaint> GetAllUserComplaint(int? id);
        public IEnumerable<User> GetAllUsers();



    }
}
