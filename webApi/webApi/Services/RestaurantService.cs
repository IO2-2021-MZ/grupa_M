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

        public void ActivateRestaurant(int? id, int userId)
        {

            if (id == null)
            {
                var uuu = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (uuu is null | uuu.Role != (int)Role.restaurateur) throw new UnathorisedException("Forbidden");
                var urs = _context.UserRests.Include(ur => ur.Restaurant).Where(ur => ur.UserId == userId).FirstOrDefault();
                if (urs is null) throw new NotFoundException("Notfound");
                id = urs.RestaurantId;
            }

            var restaurant = _context
                .Restaurants
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = (int)RestaurantState.active;
            _context.SaveChanges();
        }

        public void BlockRestaurant(int id)
        {
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = (int)RestaurantState.blocked;
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

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (section is null) throw new NotFoundException("Resources not found");
            if (user is null || !urs.Any(ur => ur.RestaurantId == section.RestaurantId)) throw new UnathorisedException("Unauthorized");

            dish.SectionId = id;
            _context.Dishes.Add(dish);
            _context.SaveChanges();

            return dish.Id;
        }

        public int CreateNewRestaurant(NewRestaurant newRestaurant, int userId, Address userAddress, int? userAdressId)
        {
            if (newRestaurant is null) throw new BadRequestException("Bad request");

            var restaurant = _mapper.Map<Restaurant>(newRestaurant);
            restaurant.State = (int)RestaurantState.disabled;
           

            restaurant.AggregatePayment = 0.0m;
            restaurant.Owing = 24.99m;

            var address = _mapper.Map<Address>(newRestaurant.Address);
            int addressId;
            Address add = _context.Addresses.FirstOrDefault(a => a.City == address.City && a.PostCode == address.PostCode && a.Street == address.Street);

            if (add is null)
            {
                if(userAddress != null && userAdressId != null)
                {
                    add = userAddress;
                    addressId = userAddress.Id;
                }else
                {
                    _context.Addresses.Add(address);
                    _context.SaveChanges();
                    addressId = address.Id;
                }
            }
            else
            {
                addressId = add.Id;
            }

            var urs = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();

            if (urs != null)
                DeleteRestaurant(urs.RestaurantId, userId);

            restaurant.AddressId = addressId;
            restaurant.DateOfJoining = DateTime.Now;
            restaurant.State = (int)RestaurantState.disabled;
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
                
                
            _context.UserRests.Add(new UserRest() { UserId = userId, RestaurantId = restaurant.Id });
            _context.SaveChanges();
            return restaurant.Id;
        }

        public int CreateSection(int? id, string sectionName, int userId)
        {

            if (id == null)
            {
                var uuu = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (uuu is null | uuu.Role != (int)Role.restaurateur) throw new UnathorisedException("Forbidden");
                var urss = _context.UserRests.Include(ur => ur.Restaurant).Where(ur => ur.UserId == userId).FirstOrDefault();
                if (urss is null) throw new NotFoundException("Notfound");
                id = urss.RestaurantId;
            }

            if (sectionName is null || sectionName == string.Empty) throw new BadRequestException("Bad request");

            var restaurant = _context.Restaurants.FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resources not found");

            var section = new Section()
            {
                Name = sectionName,
                RestaurantId = id.Value
            };

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null || (user.Role == (int)Role.restaurateur && !urs.Any(ur => ur.RestaurantId == id)))
                throw new UnathorisedException("Unauthorized");

            _context.Sections.Add(section);
            _context.SaveChanges();

            return section.Id;
        }

        public void DeactivateRestaurant(int? id, int userId)
        {
            if (id == null)
            {
                var uuu = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (uuu is null | uuu.Role != (int)Role.restaurateur) throw new UnathorisedException("Forbidden");
                var urs = _context.UserRests.Include(ur => ur.Restaurant).Where(ur => ur.UserId == userId).FirstOrDefault();
                if (urs is null) throw new NotFoundException("Notfound");
                id = urs.RestaurantId;
            }

            var restaurant = _context
                .Restaurants
                .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = (int)RestaurantState.deactivated;
            _context.SaveChanges();
        }

        public void DeleteRestaurant(int? id, int userId)
        {
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var ursOrigin = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user is null || (user.Role == (int)Role.restaurateur && !ursOrigin.Any(ur => ur.RestaurantId == id))) throw new UnathorisedException("Unauthorized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var orders = _context.Orders.Where(o => o.RestaurantId == restaurant.Id);

            var reviews = _context.Reviews.Where(o => o.RestaurantId == restaurant.Id);

            var complaints = _context.Complaints.Where(o => orders.Any(or => or.Id == o.OrderId));

            var orderDishes = _context.OrderDishes.Where(od => orders.Any(o => od.OrderId == o.Id));

            var users = _context.Users.Where(u => u.RestaurantId == restaurant.Id);

            var urs = _context.UserRests.Where(ur => ur.RestaurantId == restaurant.Id);

            foreach (var u in users)
                u.RestaurantId = null;

            _context.UserRests.RemoveRange(urs);
            _context.Reviews.RemoveRange(reviews);
            _context.Complaints.RemoveRange(complaints);
            _context.OrderDishes.RemoveRange(orderDishes);
            _context.Orders.RemoveRange(orders);

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }

        public void DeleteSection(int id, int userId)
        {
            var section = _context.Sections.Include(s => s.Dishes).ThenInclude(d => d.OrderDishes).FirstOrDefault(s => s.Id == id);

            var user = _context
                    .Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();
            
            if (section is null) throw new NotFoundException("Resources not found");

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user is null || !urs.Any(ur => ur.RestaurantId == section.RestaurantId)) throw new UnathorisedException("Unauthorized");

            foreach (var od in section.Dishes)
            {
                _context.OrderDishes.RemoveRange(od.OrderDishes);
                _context.SaveChanges();
            }
            _context.Dishes.RemoveRange(section.Dishes);
            _context.SaveChanges();
            _context.Sections.Remove(section);
            _context.SaveChanges();
        }

        public List<ComplaintR> GetAllComplaitsForRestaurants(int? id, int userId)
        {
            if (id is null)
            {
                var userr = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (userr is null) throw new UnathorisedException("Forbidde");

                Restaurant rest = null;

                if (userr.Role == (int)Role.employee)
                {
                    rest = _context.Restaurants.Where(r => r.Id == userr.RestaurantId).FirstOrDefault();
                }
                else if (userr.Role == (int)Role.restaurateur)
                {
                    var userest = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();
                    if (userest is null) throw new NotFoundException("Not found");
                    rest = _context.Restaurants.Where(r => r.Id == userest.RestaurantId).FirstOrDefault();
                }

                if (rest is null) throw new NotFoundException("Not found");
                var ccc = _context.Complaints.Include(o => o.Order).Where(c => c.Order.RestaurantId == rest.Id);
                return _mapper.Map<List<ComplaintR>>(ccc);

            }
            var restaurant = _context
                .Restaurants
                .Include(item => item.Orders)
                .FirstOrDefault(item => item.Id == id);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user is null || (user.Role == (int)Role.restaurateur && !urs.Any(ur => ur.RestaurantId == id)) || (user.Role == (int)Role.employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var complaints = _context
                .Complaints
                .Include(item => item.Order)
                .Join(_context.Orders.Where(item => item.RestaurantId == id), x => x.OrderId, y => y.Id, (x, y) => x)
                .ToList();

            List<ComplaintR> complaintDTOs = new List<ComplaintR>();
            if (user.Role == (int)Role.restaurateur || user.Role == (int)Role.employee)
            {
                complaintDTOs = _mapper.Map<List<ComplaintR>>(complaints);
            }
            else
            {
                var rests = _mapper.Map<List<ComplaintDTO>>(complaints);
                foreach (var rest in rests) complaintDTOs.Add(rest);
            }
            return complaintDTOs;
        }

        public List<OrderR> GetAllOrdersForRestaurants(int? id, int userId)
        {

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null) throw new UnathorisedException("Forbidden");

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (urs.FirstOrDefault() is null && user.Role != (int)Role.employee) return new List<OrderR>();

            if (id is null)
            {
                id = urs.FirstOrDefault().UserId;
            }

            var restaurant = _context
                .Restaurants
                .Include(item => item.Orders)
                .FirstOrDefault(item => item.Id == id);

            if (user is null || (user.Role == (int)Role.restaurateur && !urs.Any(ur => ur.RestaurantId == id)) || (user.Role == (int)Role.employee && user.RestaurantId != id)) 
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            var orderDTOs = _mapper.Map<List<OrderR>>(restaurant.Orders);

            return orderDTOs;
        }

        public List<RestaurantC> GetAllRestaurants(int? userId)
        {
            var restaurants = _context
                .Restaurants
                .Include(item => item.Address)
                .Include(item => item.Reviews)
                .ToList();
            
            var user = _context
            .Users
            .Where(u => u.Id == userId)
            .FirstOrDefault();

            List<RestaurantC> restaurantDTOs = new List<RestaurantC>();
            if (user is not null && user.Role == (int)Role.employee)
                restaurants = restaurants.Where(r => r.Id == user.RestaurantId).ToList();

            if( user is not null && user.Role == (int)Role.restaurateur)
            {
                var urs = _context.UserRests.Where(ur => ur.UserId == user.Id);
                var res = restaurants.Where(r => urs.Any(ur => ur.RestaurantId == r.Id)).FirstOrDefault();
                if (res is null)
                    return new List<RestaurantC>();
                restaurants = new List<Restaurant>() { res };
            }


            if(userId is null)
            {
                restaurants = restaurants.ToList();
                restaurantDTOs = _mapper.Map<List<RestaurantC>>(restaurants); 
            }
            else if (user.Role == (int)Role.customer)
            {
                restaurants = restaurants.Where(r => r.State == (int)RestaurantState.active).ToList();
                restaurantDTOs = _mapper.Map<List<RestaurantC>>(restaurants);
            }
            else
            {
                var rests = _mapper.Map<List<RestaurantDTO>>(restaurants);
                foreach (var rest in rests) restaurantDTOs.Add(rest);
            }

            for(int i=0; i<restaurantDTOs.Count; i++)
            {
                restaurantDTOs[i].Rating = restaurants[i].Reviews.Count == 0 ? 0: restaurants[i].Reviews.Average(r => r.Rating);
                if(user is not null && user.Role != (int)Role.customer)
                {
                    var orders = _context.Orders.Where(o => o.RestaurantId == restaurantDTOs[i].Id).ToList();
                    Func<OrderDish, decimal> func1 = od =>
                    {
                        var dish = _context.Dishes.Where(d => d.Id == od.DishId).FirstOrDefault();
                        if (dish is null) return 0;
                        return dish.Price;
                    };

                    Func<Order, decimal> func = o =>
                    {
                        var orderDishes = _context.OrderDishes.Where(od => od.OrderId == o.Id).ToList();
                        if (orderDishes.Count() == 0) return 0;
                        return orderDishes.Sum(func1);
                    };
                    ((RestaurantDTO)restaurantDTOs[i]).AggregatePayment = orders.Count() == 0 ? 0 : orders.Sum(func);
                }
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
            
            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user is null || (user.Role == (int)Role.restaurateur && !urs.Any(ur => ur.RestaurantId == id)) || (user.Role == (int)Role.employee && user.RestaurantId != id))
                throw new UnathorisedException("Unathourized");

            if (restaurant is null) throw new NotFoundException("Resource not found");

            List<ReviewR> reviewDTOs = new List<ReviewR>();
            if (user.Role == (int)Role.restaurateur || user.Role == (int)Role.employee)
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
            Restaurant restaurant;
            User user;
            UserRest urs;
            if (id != null)
            {
                restaurant = _context
                           .Restaurants
                           .Include(item => item.Address)
                           .FirstOrDefault(r => r.Id == id);

                user = _context
                   .Users
                   .Where(u => u.Id == userId)
                   .FirstOrDefault();

                urs = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();

                if (user is null || (user.Role == (int)Role.restaurateur && urs.RestaurantId != id) || (user.Role == (int)Role.employee && user.RestaurantId != id))
                    throw new UnathorisedException("Unathourized");
            }
            else
            {
                // pod grupe F obsluga bez id
                user = _context
                   .Users
                   .Where(u => u.Id == userId)
                   .FirstOrDefault();

                urs = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();

                if (!((user.Role == (int)Role.restaurateur || user.Role == (int)Role.employee) && urs != null))
                    throw new UnathorisedException("Unathourized");

                restaurant = _context
                                   .Restaurants
                                   .Include(item => item.Address)
                                   .Include(item => item.Reviews)
                                   .FirstOrDefault(r => r.Id == urs.RestaurantId);
                // ------
            }

            if (restaurant is null) throw new NotFoundException("Resource not found");

            RestaurantC restaurantDTO;
            if (user.Role == (int)Role.customer)
                restaurantDTO = _mapper.Map<RestaurantC>(restaurant);
            else
            {
                restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
                var orders = _context.Orders.Where(o => o.RestaurantId == restaurantDTO.Id).ToList();
                Func<OrderDish, decimal> func1 = od =>
                {
                    var dish = _context.Dishes.Where(d => d.Id == od.DishId).FirstOrDefault();
                    if (dish is null) return 0;
                    return dish.Price;
                };

                Func<Order,decimal> func = o =>
                {
                    var orderDishes = _context.OrderDishes.Where(od => od.OrderId == o.Id).ToList();
                    if (orderDishes.Count() == 0) return 0;
                    return orderDishes.Sum(func1);
                };
                ((RestaurantDTO)restaurantDTO).AggregatePayment = orders.Count() == 0 ? 0 : orders.Sum(func);
            }
            restaurantDTO.Rating = restaurant.Reviews.Count == 0 ? 0 : restaurant.Reviews.Average(r => r.Rating);


            return restaurantDTO;
        }

        public List<SectionDTO> GetSectionByRestaurantsId(int? id, int userId)
        {

            if(id is null)
            {
                var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user is null) throw new UnathorisedException("Forbidde");

                Restaurant rest = null;

                if(user.Role == (int)Role.employee)
                {
                    rest = _context.Restaurants.Include(r => r.Sections).Where(r => r.Id == user.RestaurantId).FirstOrDefault();
                }
                else if(user.Role == (int)Role.restaurateur)
                {
                    var userest = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();
                    if (userest is null) throw new NotFoundException("Not found");
                    rest = _context.Restaurants.Include(r => r.Sections).Where(r => r.Id == userest.RestaurantId).FirstOrDefault();
                    
                }

                if (rest is null) throw new NotFoundException("Not found");
                return _mapper.Map<List<SectionDTO>>(rest.Sections);

            }

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

        public IEnumerable<OrderR> OrdersArchive(int userId)
        {
            var rest = _context.UserRests.Where(ur => ur.UserId == userId).FirstOrDefault();

            if (rest is null) throw new NotFoundException("Not found");

            var orders = _context.Orders
                .Include(o => o.Address)
                .Include(o => o.OrderDishes)
                .Include(o => o.DiscountCode)
                .Where(o => o.RestaurantId == rest.RestaurantId).ToList();

            var ordersDTO = _mapper.Map<List<OrderR>>(orders);

            for(int i=0; i<orders.Count; i++)
            {
                var price = orders[i].OrderDishes.Sum(od => od.Dish.Price);
                var priced = orders[i].DiscountCode == null ? price : price * (1.0m - orders[i].DiscountCode.Percent / 100.0m);
                ordersDTO[i].OriginalPrice = price;
                ordersDTO[i].FinalPrice = priced;
            }

            return ordersDTO;
        }

        public void ReactivateRestaurant(int? id, int userId)
        {
            if(id == null)
            {
                var uuu = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (uuu is null | uuu.Role != (int)Role.restaurateur) throw new UnathorisedException("Forbidden");
                var urs = _context.UserRests.Include(ur => ur.Restaurant).Where(ur => ur.UserId == userId).FirstOrDefault();
                if (urs is null) throw new NotFoundException("Notfound");
                id = urs.RestaurantId;
            }
            var restaurant = _context
               .Restaurants
               .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = (int)RestaurantState.active;
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

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            var section = _context
                .Sections
                .Where(s => s.Id == dish.SectionId)
                .FirstOrDefault();

            if (section is null) throw new NotFoundException("Resource not found");

            if ((user.Role == (int)Role.restaurateur && !urs.Any(ur => ur.RestaurantId == section.RestaurantId)) || (user.Role == (int)Role.employee && user.RestaurantId != section.RestaurantId))
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

            if (user is null || user.Role != (int)Role.customer) 
                throw new UnathorisedException("Unathourized");


            _context.UserRests.Add(new UserRest() { UserId = userId, RestaurantId = id });
            _context.SaveChanges();
        }

        public void UnblockRestaurant(int id)
        {
            var restaurant = _context
               .Restaurants
               .FirstOrDefault(item => item.Id == id);

            if (restaurant is null) throw new NotFoundException("Resource not found");

            restaurant.State = (int)RestaurantState.deactivated;
            _context.SaveChanges();
        }

        public void UpdatePositionFromMenu(int id, NewPositionFromMenu newPosition, int userId)
        {
            if (newPosition is null) throw new BadRequestException("Bad request");

            var dish = _context.Dishes
                .FirstOrDefault(d => d.Id == id);

            if (dish is null) throw new NotFoundException("Resource not found");

            var section = _context.Sections
                .FirstOrDefault(d => d.Id == dish.SectionId);

            if (section is null) throw new NotFoundException("Resource not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user is null || (user.Role == (int)Role.restaurateur && !urs.Any(ur => ur.RestaurantId == section.RestaurantId)) || (user.Role == (int)Role.employee && user.RestaurantId != section.RestaurantId))
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

            var urs = _context.UserRests.Where(ur => ur.UserId == userId);

            if (user is null || (user.Role == (int)Role.restaurateur && !urs.Any(ur => ur.RestaurantId == section.RestaurantId)) || (user.Role == (int)Role.employee && user.RestaurantId != section.RestaurantId)) 
                throw new UnathorisedException("Unathourized");

            section.Name = newSectionName;
            _context.SaveChanges();


        }
    }
}
