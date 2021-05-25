using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.AuthenticateDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.UserDTO;
using webApi.Models;

namespace webApi.Services
{
    public interface IUserService
    {
        public User GetUserWithId(int? id);
        public RegisterResponse CreateNewUser(RegisterRequest newUser);

        public User CreateNewEmployee(NewEmployee value);
        public User CreateNewAdmin(NewAdministrator value);
        public User CreateNewCustomer(NewCustomer value);
        public void DeleteUser(int? id );
        public IEnumerable<OrderC> GetAllUserOrders(int id);
        public IEnumerable<Complaint> GetAllUserComplaint(int? id);
        public IEnumerable<User> GetAllUsers(int role);



    }
}
