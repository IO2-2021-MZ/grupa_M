﻿using AutoMapper;
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
using webApi.Enums;

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

        public int CreateNewPositionFromMenu(int id, NewPositionFromMenu newPosition, int userId)
        {
            if (newPosition is null) throw new BadRequestException("Bad request");

            var dish = _mapper.Map<Dish>(newPosition);

            var section = _context
                .Sections
                .FirstOrDefault(s => s.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (section is null) throw new NotFoundException("Resources not found");
            if (user is null || user.RestaurantId != section.RestaurantId) throw new UnathorisedException("Unauthorized");

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

        public int CreateSection(int id, string sectionName, int userId)
        {
            if (sectionName is null || sectionName == string.Empty) throw new BadRequestException("Bad request");

            var restaurant = _context.Restaurants.FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resources not found");

            var section = new Section()
            {
                Name = sectionName,
                RestaurantId = id
            };

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id))
                throw new UnathorisedException("Unauthorized");

            _context.Sections.Add(section);
            _context.SaveChanges();

            return section.Id;
        }

        public void DeactivateRestaurant(int id, int userId)
        {
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(item => item.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || user.RestaurantId != id) throw new UnathorisedException("Unauthorized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = 1;
            _context.SaveChanges();
        }

        public void DeleteRestaurant(int id, int userId)
        {
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id)) throw new UnathorisedException("Unauthorized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }

        public void DeleteSection(int id, int userId)
        {
            var section = _context.Sections.FirstOrDefault(s => s.Id == id);

            var user = _context
                    .Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();
            
            if (section is null) throw new NotFoundException("Resources not found");

            if (user is null ||  user.RestaurantId != section.Id) throw new UnathorisedException("Unauthorized");

            _context.Sections.Remove(section);
            _context.SaveChanges();
        }

        public List<ComplaintR> GetAllComplaitsForRestaurants(int? id, int userId)
        {
            var restaurant = _context
                .Restaurants
                .Include(item => item.Orders)
                .FirstOrDefault(item => item.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var complaints = _context
                .Complaints
                .Include(item => item.Order)
                .Join(_context.Orders.Where(item => item.RestaurantId == id), x => x.OrderId, y => y.Id, (x, y) => x)
                .ToList();

            List<ComplaintR> complaintDTOs = new List<ComplaintR>();
            if (user.Role == (int)Role.Restaurer || user.Role == (int)Role.Employee)
            {
                complaintDTOs = _mapper.Map<List<ComplaintR>>(restaurant.Reviews);
            }
            else
            {
                var rests = _mapper.Map<List<ComplaintDTO>>(restaurant.Reviews);
                foreach (var rest in rests) complaintDTOs.Add(rest);
            }
            return complaintDTOs;
        }

        public List<OrderR> GetAllOrdersForRestaurants(int id, int userId)
        {
            var restaurant = _context
                .Restaurants
                .Include(item => item.Orders)
                .FirstOrDefault(item => item.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var orderDTOs = _mapper.Map<List<OrderR>>(restaurant.Orders);

            return orderDTOs;
        }

        public List<RestaurantC> GetAllRestaurants(int userId)
        {
            var restaurants = _context
                .Restaurants
                .Include(item => item.Address)
                .ToList();
            
            var user = _context
            .Users
            .Where(u => u.Id == userId)
            .FirstOrDefault();

            List<RestaurantC> restaurantDTOs = new List<RestaurantC>();
            if (user.Role == (int)Role.Customer)
            {
                restaurantDTOs = _mapper.Map<List<RestaurantC>>(restaurants);
            }
            else
            {
                var rests = _mapper.Map<List<RestaurantDTO>>(restaurants);
                foreach (var rest in rests) restaurantDTOs.Add(rest);
            }
            return restaurantDTOs;
        }

        public List<ReviewR> GetAllReviewsForRestaurants(int? id, int userId )
        {
            var restaurant = _context
               .Restaurants
               .Include(item => item.Reviews)
               .FirstOrDefault(item => item.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            List<ReviewR> reviewDTOs = new List<ReviewR>();
            if (user.Role == (int)Role.Restaurer || user.Role == (int)Role.Employee)
            {
                reviewDTOs = _mapper.Map<List<ReviewR>>(restaurant.Reviews);
            }
            else
            {
                var rests = _mapper.Map<List<ReviewDTO>>(restaurant.Reviews);
                foreach (var rest in rests) reviewDTOs.Add(rest);
            }
            return reviewDTOs;
        }

        public RestaurantC GetRestaurantById(int? id, int userId)
        {
            var restaurant = _context
                            .Restaurants
                            .Include(item => item.Address)
                            .FirstOrDefault(r => r.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            RestaurantC restaurantDTO;
            if (user.Role == (int)Role.Customer)
                restaurantDTO = _mapper.Map<RestaurantC>(restaurant);
            else
                restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
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

        public void ReactivateRestaurant(int id, int userId)
        {
            var restaurant = _context
               .Restaurants
               .FirstOrDefault(item => item.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = 0;
            _context.SaveChanges();
        }

        public void RemovePositionFromMenu(int id, int userId)
        {
            var dish = _context.Dishes
                .FirstOrDefault(d => d.Id == id);
           
            if (dish is null) throw new NotFoundException("Resource not found");


            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null) throw new UnathorisedException("Unathourized");

            var section = _context
                .Sections
                .Where(s => s.Id == dish.SectionId)
                .FirstOrDefault();

            if (section is null) throw new NotFoundException("Resource not found");

            if ((user.Role == (int)Role.Restaurer && user.RestaurantId != section.RestaurantId) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unauthorized");


            var orderDishes = _context.OrderDishes.Where(item => item.DishId == id);
            _context.OrderDishes.RemoveRange(orderDishes);

            _context.Dishes.Remove(dish);
            _context.SaveChanges();
        }

        public void SetFavouriteRestaurant(int id, int userId)
        {
            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var restaurant = _context
                .Restaurants
                .Where(r => r.Id == id)
                .FirstOrDefault();

            if (restaurant is null) throw new NotFoundException("Resource not found");

            if (user is null || user.Role != (int)Role.Customer) 
                throw new UnathorisedException("Unathourized");

            user.RestaurantId = id;
            _context.SaveChanges();
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

        public void UpdatePositionFromMenu(int id, NewPositionFromMenu newPosition, int userId)
        {
            if (newPosition is null) throw new BadRequestException("Bad request");

            var dish = _context.Dishes
                .FirstOrDefault(d => d.Id == id);

            if (dish is null) throw new NotFoundException("Resource not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            dish.Name = newPosition.Name;
            dish.Price = newPosition.Price;
            if (newPosition.Description is not null)
                dish.Description = newPosition.Description;

            _context.SaveChanges();
        }

        public void UpdateSection(int id, string newSectionName, int userId)
        {
            if (newSectionName is null || newSectionName == string.Empty) throw new BadRequestException("Bad request");

            var section = _context.Sections.FirstOrDefault(s => s.Id == id);

            if (section is null) throw new NotFoundException("Resources not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.Restaurer && user.RestaurantId != id) || (user.Role == (int)Role.Employee && user.RestaurantId != id)) throw new UnathorisedException("Unathourized");

            section.Name = newSectionName;
            _context.SaveChanges();
        }
    }
}
