using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
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

        public int CreateNewDiscountCode(NewDiscountCode newDiscountCode)
        {
            var ndc = _mapper.Map<DiscountCode>(newDiscountCode);
            _context.DiscountCodes.Add(ndc);
            _context.SaveChanges();

            return ndc.Id;
        }

        public bool DeleteDiscountCode(int id)
        {
            var codeToDelete = _context.DiscountCodes.FirstOrDefault(dc => dc.Id == id);

            if (codeToDelete == null) return false;

            _context.DiscountCodes.Remove(codeToDelete);
            return true;
        }

        public IEnumerable<DiscountCode> GetAllDiscountCodes()
        {
            var queryResult = (from Codes in _context.DiscountCodes select Codes).OrderBy(x => x.Id).ToList();
            return queryResult;
        }

        public DiscountCode GetDiscountCodeById(int? id)
        {
            if(id == null) return null;

            return _context.DiscountCodes.FirstOrDefault(code => code.Id == id.Value);
        }
    }
}
