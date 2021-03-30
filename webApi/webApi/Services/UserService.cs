using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.UserDTO;

namespace webApi.Services
{

    public interface IUserService
    {
        public UserDTO GetUserWithId(int? id);
    }
    public class UserService : IUserService
    {
        Models.IO2_RestaurantsContext _context;
        private readonly IMapper _mapper;
        public UserService(Models.IO2_RestaurantsContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public UserDTO GetUserWithId(int? id)
        {
            //return _context.Users.FirstOrDefault(user => user.Id == id);
            throw new NotImplementedException();
        }
    }
}
