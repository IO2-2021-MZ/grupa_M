using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.Order;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
using webApi.Models;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurant([FromRoute] int id)
        {
            Restaurant restaurant = _restaurantService.GetRestaurantById(id);
            return Ok(restaurant);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] NewRestaurant newRestaurant)
        {
            int id = _restaurantService.CreateNewRestaurant(newRestaurant);
            return Created($"/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant([FromRoute] int id)
        {
            _restaurantService.DeleteRestaurant(id);
            return NoContent();
        }

        [HttpGet("menu/{id}")]
        public ActionResult<Section> GetSectionByRestaurantsId([FromQuery] int id)
        {
            Section section = _restaurantService.GetSectionByRestaurantsId(id);
            return Ok(section);
        }

        [HttpPost("menu/section")]
        public ActionResult CreateSection([FromQuery] string section)
        {
            int id = _restaurantService.CreateSection(section);
            return Created($"/restaurant/menu/section/{id}", null);
        }

        [HttpPatch("menu/section/{id}")]
        public ActionResult UpdateSection([FromQuery] int id, [FromBody] string newName)
        {
            _restaurantService.UpdateSection(id, newName);
            return Ok();
        }

        [HttpDelete("menu/section/{id}")]
        public ActionResult DeleteSection([FromQuery] int id)
        {
            _restaurantService.DeleteSection(id);
            return Ok();
        }

        [HttpPost("menu/position/{id}")]
        public ActionResult CreatePosition([FromQuery] int id, [FromBody] NewPositionFromMenu newPosition)
        {
            int positionId = _restaurantService.CreateNewPositionFromMenu(id, newPosition);
            return Created($"restaurant/menu/position/{positionId}", null);
        }

        [HttpPatch("menu/position/{id}")]
        public ActionResult UpdatePositionFromMenu([FromQuery] int id, NewPositionFromMenu newPosition)
        {
            _restaurantService.UpdatePositionFromMenu(id, newPosition);
            return Ok();
        }

        [HttpDelete("menu/position/{id}")]
        public ActionResult DeletePositionFromMenu([FromQuery] int id)
        {
            _restaurantService.RemovePositionFromMenu(id);
            return NoContent();
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            IEnumerable<Restaurant> restaurants = _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("order/all/{id}")]
        public ActionResult<IEnumerable<OrderR>> GetAllOrdersForRestaurant([FromQuery] int id)
        {
            IEnumerable<OrderR> orders = _restaurantService.GetAllOrdersForRestaurants(id);
            return Ok(orders);
        }

        [HttpGet("review/all/{id}")]
        public ActionResult<IEnumerable<OrderR>> GetAllReviewsForRestaurant([FromQuery] int id)
        {
            IEnumerable<ReviewR> reviews = _restaurantService.GetAllReviewsForRestaurants(id);
            return Ok(reviews);
        }

        [HttpGet("complaint/all/{id}")]
        public ActionResult<IEnumerable<ComplaintR>> GetAllComplaintsForRestaurant([FromQuery] int id)
        {
            IEnumerable<ComplaintR> complaints = _restaurantService.GetAllComplaitsForRestaurants(id);
            return Ok(complaints);
        }

        [HttpPost("favourite/{id}")]
        public ActionResult SetFavouriteRestaurant([FromQuery] int id)
        {
            _restaurantService.SetFavouriteRestaurant(id);
            return Ok();
        }

        [HttpPost("activate/{id}")]
        public ActionResult ActivateRestaurant([FromQuery] int id)
        {
            _restaurantService.ActivateRestaurant(id);
            return Ok();
        }

        [HttpPost("reactivate/{id}")]
        public ActionResult ReactivateRestaurant([FromQuery] int id)
        {
            _restaurantService.ReactivateRestaurant(id);
            return Ok();
        }

        [HttpPost("deativate/{id}")]
        public ActionResult DeactivateRestaurant([FromQuery] int id)
        {
            _restaurantService.DeactivateRestaurant(id);
            return Ok();
        }

        [HttpPost("block/{id}")]
        public ActionResult BlockRestaurant([FromQuery] int id)
        {
            _restaurantService.BlockRestaurant(id);
            return Ok();
        }

        [HttpPost("unblock/{id}")]
        public ActionResult UnblockRestaurant([FromQuery] int id)
        {
            _restaurantService.UnblockRestaurant(id);
            return Ok();
        }
    }
}
