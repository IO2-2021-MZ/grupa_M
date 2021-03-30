using System;
using System.Collections.Generic;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.DataTransferObjects.SectionDTO;

namespace webApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private Models.IO2_RestaurantsContext _context;
        public RestaurantService(Models.IO2_RestaurantsContext context)
        {
            _context = context;
        }

        public void ActivateRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public void BlockRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public int CreateNewPositionFromMenu(int id, NewPositionFromMenu newPosition)
        {
            throw new NotImplementedException();
        }

        public int CreateNewRestaurant(NewRestaurant newRestaurant)
        {
            throw new NotImplementedException();
        }

        public int CreateSection(string sectionName)
        {
            throw new NotImplementedException();
        }

        public void DeactivateRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteSection(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComplaintR> GetAllComplaitsForRestaurants(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderR> GetAllOrdersForRestaurants(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantDTO> GetAllRestaurants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReviewR> GetAllReviewsForRestaurants(int? id)
        {
            throw new NotImplementedException();
        }

        public RestaurantDTO GetRestaurantById(int? id)
        {
            throw new NotImplementedException();
        }

        public SectionDTO GetSectionByRestaurantsId(int id)
        {
            throw new NotImplementedException();
        }

        public void ReactivateRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public void RemovePositionFromMenu(int id)
        {
            throw new NotImplementedException();
        }

        public void SetFavouriteRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public void UnblockRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePositionFromMenu(int id, NewPositionFromMenu newPosition)
        {
            throw new NotImplementedException();
        }

        public void UpdateSection(int id, string newSectionName)
        {
            throw new NotImplementedException();
        }
        public int CreateSection(int id, string sectionName)
        {
            throw new NotImplementedException();
        }

    }
}
