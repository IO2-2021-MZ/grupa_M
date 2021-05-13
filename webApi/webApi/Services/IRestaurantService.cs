using System.Collections.Generic;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.DataTransferObjects.SectionDTO;

namespace webApi.Services
{
    public interface IRestaurantService
    {
        RestaurantC GetRestaurantById(int? id, int userId);
        int CreateNewRestaurant(NewRestaurant newRestaurant);
        void DeleteRestaurant(int id, int userId);
        List<SectionDTO> GetSectionByRestaurantsId(int id);
        int CreateSection(int id, string sectionName, int userId);
        void UpdateSection(int id, string newSectionName, int userId);
        void DeleteSection(int id, int userId);
        int CreateNewPositionFromMenu(int id, NewPositionFromMenu newPosition, int userId);
        void UpdatePositionFromMenu(int id, NewPositionFromMenu newPosition, int userId);
        void RemovePositionFromMenu(int id, int userId);
        List<RestaurantC> GetAllRestaurants(int userId);
        List<OrderR> GetAllOrdersForRestaurants(int id, int userId);
        List<ReviewDTO> GetAllReviewsForRestaurants(int? id, int userId);
        List<ComplaintDTO> GetAllComplaitsForRestaurants(int? id, int userId);
        void SetFavouriteRestaurant(int id, int userId);
        void ActivateRestaurant(int id);
        void ReactivateRestaurant(int id, int userId);
        void DeactivateRestaurant(int id, int userId);
        void BlockRestaurant(int id);
        void UnblockRestaurant(int id);
    }
}