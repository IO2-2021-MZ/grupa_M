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
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = 0;
            _context.SaveChanges();
        }

        public void BlockRestaurant(int id)
        {
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = 2;
            _context.SaveChanges();
        }

        public int CreateNewPositionFromMenu(int id, NewPositionFromMenu newPosition)
        {
            if (newPosition is null) throw new BadRequestException("Bad request");

            var dish = _mapper.Map<Dish>(newPosition);

            var section = _context
                .Sections
                .FirstOrDefault(s => s.Id == id);

            if (section is null) throw new NotFoundException("Resources not found");

            _context.Dishes.Add(dish);
            _context.SaveChanges();

            return dish.Id;
        }

        public int CreateNewRestaurant(NewRestaurant newRestaurant)
        {
            if (newRestaurant is null) throw new BadRequestException("Bad request");

            var restaurant = _mapper.Map<Restaurant>(newRestaurant);
            restaurant.State = 1;
            var address = _mapper.Map<Address>(newRestaurant.Address);
            int addressId;

            Address add = _context.Addresses.FirstOrDefault(a => a.City == address.City && a.PostCode == address.PostCode && a.Street == address.Street);

            if (add is null)
            {
                _context.Addresses.Add(address);
                _context.SaveChanges();
                addressId = address.Id;
            }
            else
            {
                addressId = add.Id;
            }

            restaurant.AddressId = addressId;
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant.Id;
        }

        public int CreateSection(int id, string sectionName)
        {
            if (sectionName is null || sectionName == string.Empty) throw new BadRequestException("Bad request");

            var restaurant = _context.Restaurants.FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resources not found");

            var section = new Section()
            {
                Name = sectionName,
                RestaurantId = id
            };

            _context.Sections.Add(section);
            _context.SaveChanges();

            return section.Id;
        }

        public void DeactivateRestaurant(int id)
        {
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = 1;
            _context.SaveChanges();
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
            var section = _context.Sections.FirstOrDefault(s => s.Id == id);

            if (section is null) throw new NotFoundException("Resources not found");

            _context.Sections.Remove(section);
            _context.SaveChanges();
        }

        public IEnumerable<ComplaintDTO> GetAllComplaitsForRestaurants(int? id)
        {
            var restaurant = _context
                .Restaurants
                .Include(item => item.Orders)
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var complaints = _context
                .Complaints
                .Include(item => item.Order)
                .Join(_context.Orders.Where(item => item.RestaurantId == id), x => x.OrderId, y => y.Id, (x, y) => x)
                .ToList();

            var complaintDTOs = _mapper.Map<List<ComplaintDTO>>(complaints);
            return complaintDTOs;
        }

        public IEnumerable<OrderDTO> GetAllOrdersForRestaurants(int id)
        {
            var restaurant = _context
                .Restaurants
                .Include(item => item.Orders)
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var orderDTOs = _mapper.Map<List<OrderDTO>>(restaurant.Orders);

            return orderDTOs;
        }

        public IEnumerable<RestaurantDTO> GetAllRestaurants()
        {
            var restaurants = _context
                .Restaurants
                .Include(item => item.Address)
                .ToList();

            var restaurantDTOs = _mapper.Map<List<RestaurantDTO>>(restaurants);
            return restaurantDTOs;
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForRestaurants(int? id)
        {
            var restaurant = _context
               .Restaurants
               .Include(item => item.Reviews)
               .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var reviewDTOs = _mapper.Map<List<ReviewDTO>>(restaurant.Orders);

            return reviewDTOs;
        }

        public RestaurantDTO GetRestaurantById(int? id)
        {
            var restaurant = _context
                            .Restaurants
                            .Include(item => item.Address)
                            .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
            return restaurantDTO;
        }

        public List<SectionDTO> GetSectionByRestaurantsId(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found!");

            var sections = _context.Sections
                            .Where(s => s.RestaurantId == id)
                            .Include(s => s.Dishes)
                            .ToList();

            if (sections is null) throw new NotFoundException("Resources not found");

            var sectionDTOs = _mapper.Map<List<SectionDTO>>(sections);
            return sectionDTOs;

        }

        public void ReactivateRestaurant(int id)
        {
            var restaurant = _context
               .Restaurants
               .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = 0;
            _context.SaveChanges();
        }

        public void RemovePositionFromMenu(int id)
        {
            var dish = _context.Dishes
                .FirstOrDefault(d => d.Id == id);

            if (dish is null) throw new NotFoundException("Resource not found");

            _context.Dishes.Remove(dish);
            _context.SaveChanges();
        }

        public void SetFavouriteRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public void UnblockRestaurant(int id)
        {
            var restaurant = _context
               .Restaurants
               .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = 1;
            _context.SaveChanges();
        }

        public void UpdatePositionFromMenu(int id, NewPositionFromMenu newPosition)
        {
            if (newPosition is null) throw new BadRequestException("Bad request");

            var dish = _context.Dishes
                .FirstOrDefault(d => d.Id == id);

            if (dish is null) throw new NotFoundException("Resource not found");

            dish.Name = newPosition.Name;
            dish.Price = newPosition.Price;
            if (newPosition.Description is not null)
                dish.Description = newPosition.Description;

            _context.SaveChanges();
        }

        public void UpdateSection(int id, string newSectionName)
        {
            if (newSectionName is null || newSectionName == string.Empty) throw new BadRequestException("Bad request");

            var section = _context.Sections.FirstOrDefault(s => s.Id == id);

            if (section is null) throw new NotFoundException("Resources not found");

            section.Name = newSectionName;
            _context.SaveChanges();
        }
    }
}
