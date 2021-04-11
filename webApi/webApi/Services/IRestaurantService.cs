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
        RestaurantDTO GetRestaurantById(int? id);
        int CreateNewRestaurant(NewRestaurant newRestaurant);
        void DeleteRestaurant(int id);
        List<SectionDTO> GetSectionByRestaurantsId(int id);
        int CreateSection(int id, string sectionName);
        void UpdateSection(int id, string newSectionName);
        void DeleteSection(int id);
        int CreateNewPositionFromMenu(int id, NewPositionFromMenu newPosition);
        void UpdatePositionFromMenu(int id, NewPositionFromMenu newPosition);
        void RemovePositionFromMenu(int id);
        IEnumerable<RestaurantDTO> GetAllRestaurants();
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