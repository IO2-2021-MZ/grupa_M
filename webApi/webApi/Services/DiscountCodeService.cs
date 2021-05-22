using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.Enums;
using webApi.Exceptions;
using webApi.Models;

namespace webApi.Services
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private IO2_RestaurantsContext _context;
        private readonly IMapper _mapper;

        public DiscountCodeService(IO2_RestaurantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int CreateNewDiscountCode(NewDiscountCode newDiscountCode, int userId)
        {
            if (newDiscountCode is null) throw new BadRequestException("Bad request");

            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (user is null) throw new UnathorisedException("Unauthorized");

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user.Role == (int)Role.Restaurer && !urs.Any(ur => ur.RestaurantId == newDiscountCode.RestaurantId))
                throw new UnathorisedException("Forbidden");

            if (user.Role == (int)Role.Employee && user.RestaurantId != newDiscountCode.RestaurantId)
                throw new UnathorisedException("Forbidden");

            var ndc = _mapper.Map<DiscountCode>(newDiscountCode);
            _context.DiscountCodes.Add(ndc);
            _context.SaveChanges();

            return ndc.Id;
        }

        public bool DeleteDiscountCode(int id, int userId)
        {
            var codeToDelete = _context.DiscountCodes.FirstOrDefault(dc => dc.Id == id);

            if (codeToDelete is null) throw new NotFoundException("Resource not found");

            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (user is null) throw new UnathorisedException("Unauthorized");

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user.Role == (int)Role.Restaurer && !urs.Any(ur => ur.RestaurantId == codeToDelete.RestaurantId))
                throw new UnathorisedException("Forbidden");

            if (user.Role == (int)Role.Employee && user.RestaurantId != newDiscountCode.RestaurantId)
                throw new UnathorisedException("Forbidden");

            var orders = _context.Orders.Where(o => o.DiscountCodeId == codeToDelete.Id);

            foreach (var o in orders)
                o.DiscountCodeId = null;

            _context.DiscountCodes.Remove(codeToDelete);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<DiscountCodeDTO> GetAllDiscountCodes(int userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (user is null) throw new UnathorisedException("Unauthorized");

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            var queryResult = (from Codes in _context.DiscountCodes select Codes).OrderBy(x => x.Id).ToList();

            if(user.Role == (int)Role.Restaurer)
            {
                queryResult = queryResult.Where(code => urs.Any(ur => ur.RestaurantId == code.RestaurantId)).ToList();
            }

            if (user.Role == (int)Role.Employee)
            {
                queryResult = queryResult.Where(code => code.RestaurantId == user.RestaurantId).ToList();
            }

            var result = _mapper.Map<List<DiscountCodeDTO>>(queryResult);
            return result;
        }

        public DiscountCodeDTO GetDiscountCodeByCode(string? code, int userId)
        {
            var dc = _context
                            .DiscountCodes
                            .FirstOrDefault(r => r.Code == code);

            if (dc is null) throw new NotFoundException("Resource not found");

            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (user is null) throw new UnathorisedException("Unauthorized");

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user.Role == (int)Role.Restaurer && !urs.Any(ur => ur.RestaurantId == dc.RestaurantId))
                throw new UnathorisedException("Forbidden");

            if (user.Role == (int)Role.Employee && user.RestaurantId != dc.RestaurantId)
                throw new UnathorisedException("Forbidden");

            var dcDTO = _mapper.Map<DiscountCodeDTO>(dc);
            return dcDTO;
        }
    }
}
