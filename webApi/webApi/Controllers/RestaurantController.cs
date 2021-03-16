﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Returns Restaurant Details
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Returns Restaurant Details </returns>
        /// <response code="200">Return restaurant details</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpGet]
        public ActionResult<Restaurant> GetRestaurant([FromQuery] int? id)
        {
            Restaurant restaurant = _restaurantService.GetRestaurantById(id);
            return Ok(restaurant);
        }

        /// <summary>
        /// Creates New Restaurant
        /// </summary>
        /// <returns> Create New Restaurant</returns>
        /// <response code="200">Restaurant successfully added</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] NewRestaurant newRestaurant)
        {
            int id = _restaurantService.CreateNewRestaurant(newRestaurant);
            return Created($"/restaurant/{id}", null);
        }

        /// <summary>
        /// Deletes Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Delete Restaurant </returns>
        /// <response code="200">Restaurant deleted</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpDelete]
        public ActionResult DeleteRestaurant([FromQuery] int id)
        {
            _restaurantService.DeleteRestaurant(id);
            return NoContent();
        }

        /// <summary>
        /// Returns Menu Details
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Returns Menu Details </returns>
        /// <response code="200">Return menu details</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpGet("menu")]
        public ActionResult<Section> GetSectionByRestaurantsId([FromQuery] int id)
        {
            Section section = _restaurantService.GetSectionByRestaurantsId(id);
            return Ok(section);
        }

        /// <summary>
        /// Creates New Section
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <param name="section">New Section Name</param>
        /// <returns> Creates New Section </returns>
        /// <response code="200">New Section Created</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpPost("menu/section")]
        public ActionResult CreateSection([FromQuery] int id,[FromQuery] string section)
        {
            //id z tokenu po zalogowaniu
            int sectionId = _restaurantService.CreateSection(id, section);
            return Created($"/restaurant/menu/section/{sectionId}", null);
        }

        /// <summary>
        /// Updates Section
        /// </summary>
        /// <param name="id"> Section Id </param>
        /// <param name="newName">Section New Name</param>
        /// <returns> Updates Section </returns>
        /// <response code="200">New Section Updated</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPatch("menu/section")]
        public ActionResult UpdateSection([FromQuery] int id, [FromBody] string newName)
        {
            _restaurantService.UpdateSection(id, newName);
            return Ok();
        }

        /// <summary>
        /// Deletes Section
        /// </summary>
        /// <param name="id"> Section Id </param>
        /// <returns> Deletes Section </returns>
        /// <response code="200">Section Deleted</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpDelete("menu/section")]
        public ActionResult DeleteSection([FromQuery] int id)
        {
            _restaurantService.DeleteSection(id);
            return Ok();
        }

        /// <summary>
        /// Creates New Dish
        /// </summary>
        /// <param name="id"> Section Id </param>
        /// <param name="newPosition">New Position</param>
        /// <returns> Creates New Dish</returns>
        /// <response code="200">New Dish Created</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpPost("menu/position")]
        public ActionResult CreatePosition([FromQuery] int id, [FromBody] NewPositionFromMenu newPosition)
        {
            int positionId = _restaurantService.CreateNewPositionFromMenu(id, newPosition);
            return Created($"restaurant/menu/position/{positionId}", null);
        }

        /// <summary>
        /// Updates Dish
        /// </summary>
        /// <param name="id"> Dish Id </param>
        /// <param name="newPosition">New Position</param>
        /// <returns> Updates Dish </returns>
        /// <response code="200">Dish Updated</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPatch("menu/position")]
        public ActionResult UpdatePositionFromMenu([FromQuery] int id, NewPositionFromMenu newPosition)
        {
            _restaurantService.UpdatePositionFromMenu(id, newPosition);
            return Ok();
        }

        /// <summary>
        /// Deletes Dish
        /// </summary>
        /// <param name="id"> Dish Id </param>
        /// <returns> Deletes Dish </returns>
        /// <response code="200">Dish deleted</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpDelete("menu/position")]
        public ActionResult DeletePositionFromMenu([FromQuery] int id)
        {
            _restaurantService.RemovePositionFromMenu(id);
            return NoContent();
        }

        /// <summary>
        /// Returns All Restaurants
        /// </summary>
        /// <returns> Returns All Restaurants </returns>
        /// <response code="200">Returns all restaurants </response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpGet("all")]
        public ActionResult<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            IEnumerable<Restaurant> restaurants = _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }

        /// <summary>
        /// Returns Orders For Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Returns Orders For Restaurant </returns>
        /// <response code="200">Returns orders for restaurant</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpGet("order/all")]
        public ActionResult<IEnumerable<OrderR>> GetAllOrdersForRestaurant([FromQuery] int id)
        {
            //id z tokenu po zalogowaniu
            IEnumerable<OrderR> orders = _restaurantService.GetAllOrdersForRestaurants(id);
            return Ok(orders);
        }

        /// <summary>
        /// Returns Reviews For Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Returns Reviews For Restaurant </returns>
        /// <response code="200">Returns reviews for restaurant</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpGet("review/all")]
        public ActionResult<IEnumerable<OrderR>> GetAllReviewsForRestaurant([FromQuery] int? id)
        {
            IEnumerable<ReviewR> reviews = _restaurantService.GetAllReviewsForRestaurants(id);
            return Ok(reviews);
        }

        /// <summary>
        /// Returns Complaints For Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Returns Complaints For Restaurant </returns>
        /// <response code="200">Returns complaints for restaurant</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpGet("complaint/all")]
        public ActionResult<IEnumerable<ComplaintR>> GetAllComplaintsForRestaurant([FromQuery] int? id)
        {
            IEnumerable<ComplaintR> complaints = _restaurantService.GetAllComplaitsForRestaurants(id);
            return Ok(complaints);
        }

        /// <summary>
        /// Sets Favourite Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Sets Favourite Restaurant </returns>
        /// <response code="200">Favourite Restaurant Set</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("favourite")]
        public ActionResult SetFavouriteRestaurant([FromQuery] int id)
        {
            _restaurantService.SetFavouriteRestaurant(id);
            return Ok();
        }

        /// <summary>
        /// Activates Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Activates Restaurant </returns>
        /// <response code="200">Restaurant activated</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("activate")]
        public ActionResult ActivateRestaurant([FromQuery] int id)
        {
            _restaurantService.ActivateRestaurant(id);
            return Ok();
        }

        /// <summary>
        /// Reactivates Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Reactivates Restaurant </returns>
        /// <response code="200">Restaurant reactivated</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpPost("reactivate")]
        public ActionResult ReactivateRestaurant([FromQuery] int id)
        {
            //id z tokenu po zalogowaniu
            _restaurantService.ReactivateRestaurant(id);
            return Ok();
        }

        /// <summary>
        /// Deactivates Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Deactivates Restaurant </returns>
        /// <response code="200">Restaurant Deactivated</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpPost("deativate")]
        public ActionResult DeactivateRestaurant([FromQuery] int id)
        {
            //id z tokenu po zalogowaniu
            _restaurantService.DeactivateRestaurant(id);
            return Ok();
        }

        /// <summary>
        /// Blocks Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Blocks Restaurant </returns>
        /// <response code="200">Restaurant blocked</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("block")]
        public ActionResult BlockRestaurant([FromQuery] int id)
        {
            _restaurantService.BlockRestaurant(id);
            return Ok();
        }

        /// <summary>
        /// Unblocks Restaurant
        /// </summary>
        /// <param name="id"> Restaurant Id </param>
        /// <returns> Unblocks Restaurant </returns>
        /// <response code="200">Restaurant unblocked</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("unblock")]
        public ActionResult UnblockRestaurant([FromQuery] int id)
        {
            _restaurantService.UnblockRestaurant(id);
            return Ok();
        }
    }
}
