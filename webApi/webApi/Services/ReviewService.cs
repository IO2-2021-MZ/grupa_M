using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.ReviewDBO;
using webApi.Models;

namespace webApi.Services
{
    public class ReviewService : IReviewService
    {
        private IO2_RestaurantsContext _context;
        private readonly IMapper _mapper;
        public ReviewService(IO2_RestaurantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int CreateNewReview(NewReview newReview)
        {
            var nr = _mapper.Map<Review>(newReview);
            _context.Reviews.Add(nr);
            _context.SaveChanges();

            return nr.Id;
        }

        public bool DeleteReview(int id)
        {
            var reviewToDelete = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (reviewToDelete == null) return false;

            _context.Reviews.Remove(reviewToDelete);
            return true;
        }

        public Review GetReviewById(int? id)
        {
            if (id == null) return null;

            return _context.Reviews.FirstOrDefault(code => code.Id == id.Value);
        }
    }
}
