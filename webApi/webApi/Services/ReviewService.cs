using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.Exceptions;
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

        public int CreateNewReview(NewReview newReview, int userId)
        {
            if (newReview is null) throw new BadRequestException("Bad request");
            var nr = _mapper.Map<Review>(newReview);
            nr.CustomerId = userId;

            _context.Reviews.Add(nr);
            _context.SaveChanges();

            return nr.Id;
        }

        public bool DeleteReview(int id)
        {      
            var reviewToDelete = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (reviewToDelete is null) throw new NotFoundException("Resource not found");

            _context.Reviews.Remove(reviewToDelete);
            _context.SaveChanges();

            return true;
        }

        public ReviewDTO GetReviewById(int? id)
        {
            var review = _context
                .Reviews
                .FirstOrDefault(r => r.Id == id);

            if (review is null) throw new NotFoundException("Resource not found");

            var reviewDTO = _mapper.Map<ReviewDTO>(review);
            return reviewDTO;
        }
    }
}
