using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.DataTransferObjects.SectionDTO;
using webApi.Exceptions;
using webApi.Models;

namespace webApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IO2_RestaurantsContext _context;
        private readonly IMapper _mapper;

        public RestaurantService(IO2_RestaurantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var dish = _mapper.Map<Dish>(newPosition);

            var section = _context
                .Sections
                .FirstOrDefault(s => s.Id == id);

            if (section is null) throw new NotFoundException("Resources not found");

            _context.Dishes.Add(dish);

            var sectionDish = new SectionDish()
            {
                DishId = dish.Id,
                SectionId = section.Id
            };

            return dish.Id;
        }

        public int CreateNewRestaurant(NewRestaurant newRestaurant)
        {
            var restaurant = _mapper.Map<Restaurant>(newRestaurant);
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant.Id;
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
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
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
            var restaurant = _context
                            .Restaurants
                            .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
            return restaurantDTO;
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
