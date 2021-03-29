using System.Collections.Generic;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
using webApi.Models;

namespace webApi.Services
{
    public interface IReviewService
    {
        public Review GetReviewById(int? id);
        int CreateNewReview(NewReview newReview);
        void DeleteReview(int id);
    }
}