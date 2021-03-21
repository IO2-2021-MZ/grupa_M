using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
using webApi.Models;

namespace webApi.Services
{
    public class ReviewService : IReviewService
    {
        private IO2_RestaurantsContext _context;
        public ReviewService(IO2_RestaurantsContext context)
        {
            _context = context;
        }

        public int CreateNewReview(NewReview newReview)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        public Review GetReviewById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}