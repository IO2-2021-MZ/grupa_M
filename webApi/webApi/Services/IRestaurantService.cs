using System.Collections.Generic;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
using webApi.DataTransferObjects.Section;

namespace webApi.Services
{
    public interface IRestaurantService
    {
        TransferRestaurant GetRestaurantById(int? id);
        int CreateNewRestaurant(NewRestaurant newRestaurant);
        void DeleteRestaurant(int id);
        TransferSection GetSectionByRestaurantsId(int id);
        int CreateSection(int id, string sectionName);
        void UpdateSection(int id, string newSectionName);
        void DeleteSection(int id);
        int CreateNewPositionFromMenu(int id, NewPositionFromMenu newPosition);
        void UpdatePositionFromMenu(int id, NewPositionFromMenu newPosition);
        void RemovePositionFromMenu(int id);
        IEnumerable<TransferRestaurant> GetAllRestaurants();
        IEnumerable<OrderR> GetAllOrdersForRestaurants(int id);
        IEnumerable<ReviewR> GetAllReviewsForRestaurants(int? id);
        IEnumerable<ComplaintR> GetAllComplaitsForRestaurants(int? id);
        void SetFavouriteRestaurant(int id);
        void ActivateRestaurant(int id);
        void ReactivateRestaurant(int id);
        void DeactivateRestaurant(int id);
        void BlockRestaurant(int id);
        void UnblockRestaurant(int id);
    }
}