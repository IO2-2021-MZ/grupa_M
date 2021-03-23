using System.Collections.Generic;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;

namespace webApi.Services
{
    public interface IReviewService
    {
        public Review GetReviewById(int? id);
        int CreateNewReview(NewReview newReview);
        void DeleteReview(int id);
    }
}