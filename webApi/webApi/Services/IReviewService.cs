using System.Collections.Generic;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.Models;

namespace webApi.Services
{
    public interface IReviewService
    {
        public ReviewDTO GetReviewById(int? id);
        int CreateNewReview(NewReview newReview);
        bool DeleteReview(int id);
    }
}