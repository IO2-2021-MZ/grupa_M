using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.Services
{

    public interface IUserService
    {
        public User GetUserWithId(int id);
    }
    public class UserService : IUserService
    {
        IO2_RestaurantsContext _context;

        public UserService(IO2_RestaurantsContext context)
        {
            _context = context;
        }
        public User GetUserWithId(int id)
        {
            return _context.Users.Include(user => user.Address).FirstOrDefault(user => user.Id == id);
        }
    }
}
