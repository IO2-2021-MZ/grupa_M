using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.DataTransferObjects.SectionDTO;
using webApi.Services;
using webApi.Enums;

namespace webApi.Controllers
{
    [ApiController]
    [Route("restaurant")]
    [EnableCors("AllowOrigin")]
    public class RestaurantController : AuthenticativeController
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur, Role.employee)]
        public ActionResult<RestaurantC> GetRestaurant([FromQuery] int? id)
        {
            RestaurantC restaurant = _restaurantService.GetRestaurantById(id, Account.Id);
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
        [Authorize(Role.restaurateur)]
        public ActionResult CreateRestaurant([FromBody] NewRestaurant newRestaurant)
        {
            int id = _restaurantService.CreateNewRestaurant(newRestaurant, Account.Id, Account.Address, Account.AddressId);
            return Ok(id);
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
        [Authorize(Role.admin, Role.restaurateur)]
        public ActionResult DeleteRestaurant([FromQuery] int id)
        {
            _restaurantService.DeleteRestaurant(id, Account.Id);
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur, Role.employee)]
        public ActionResult<List<SectionDTO>> GetSectionByRestaurantsId([FromQuery] int? id)
        {
            List<SectionDTO> sections = _restaurantService.GetSectionByRestaurantsId(id, Account.Id);
            return Ok(sections);
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
        [Authorize(Role.restaurateur)]
        public ActionResult CreateSection([FromQuery] int? id,[FromQuery] string section)
        {
            //id z tokenu po zalogowaniu
            int sectionId = _restaurantService.CreateSection(id, section, Account.Id);
            return Ok($"/restaurant/menu/section/{sectionId}");
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
        [Authorize(Role.restaurateur)]
        public ActionResult UpdateSection([FromQuery] int id, [FromBody] string newName)
        {
            _restaurantService.UpdateSection(id, newName, Account.Id);
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
        [Authorize(Role.restaurateur)]
        public ActionResult DeleteSection([FromQuery] int id)
        {
            _restaurantService.DeleteSection(id, Account.Id);
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
        [Authorize(Role.restaurateur)]
        public ActionResult CreatePosition([FromQuery] int id, [FromBody] NewPositionFromMenu newPosition)
        {
            int positionId = _restaurantService.CreateNewPositionFromMenu(id, newPosition, Account.Id);
            return Ok($"restaurant/menu/position/{positionId}");
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
        [Authorize(Role.restaurateur)]
        public ActionResult UpdatePositionFromMenu([FromQuery] int id, [FromBody]NewPositionFromMenu newPosition)
        {
            _restaurantService.UpdatePositionFromMenu(id, newPosition, Account.Id);
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
        [Authorize(Role.restaurateur)]
        public ActionResult DeletePositionFromMenu([FromQuery] int id)
        {
            _restaurantService.RemovePositionFromMenu(id, Account.Id);
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
        public ActionResult<IEnumerable<RestaurantC>> GetAllRestaurants()
        {
            IEnumerable<RestaurantC> restaurants = _restaurantService.GetAllRestaurants(Account?.Id);
            return Ok(restaurants);
        }
        [HttpGet("order/archive")]
        [Authorize(Role.restaurateur)]
        public ActionResult<IEnumerable<OrderR>> GetOrdersArchive()
        {
            IEnumerable<OrderR> orders = _restaurantService.OrdersArchive(Account.Id);
            return Ok(orders);
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
        [Authorize(Role.restaurateur, Role.employee)]
        public ActionResult<IEnumerable<OrderR>> GetAllOrdersForRestaurant([FromQuery] int? id)
        {
            //id z tokenu po zalogowaniu
            IEnumerable<OrderR> orders = _restaurantService.GetAllOrdersForRestaurants(id, Account.Id);
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
        [Authorize(Role.admin, Role.restaurateur, Role.customer, Role.employee)]
        public ActionResult<List<ReviewR>> GetAllReviewsForRestaurant([FromQuery] int? id)
        {
            List<ReviewR> reviews = _restaurantService.GetAllReviewsForRestaurants(id, Account.Id);
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
        [Authorize(Role.admin, Role.restaurateur, Role.employee)]
        public ActionResult<List<ComplaintR>> GetAllComplaintsForRestaurant([FromQuery] int? id)
        {
            List<ComplaintR> complaints = _restaurantService.GetAllComplaitsForRestaurants(id, Account.Id);
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
        [Authorize(Role.customer)]
        public ActionResult SetFavouriteRestaurant([FromQuery] int id)
        {
            _restaurantService.SetFavouriteRestaurant(id, Account.Id);
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
        [Authorize(Role.admin, Role.restaurateur)]
        public ActionResult ActivateRestaurant([FromQuery] int? id)
        {
            _restaurantService.ActivateRestaurant(id, Account.Id);
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
        [Authorize(Role.admin, Role.restaurateur)]
        public ActionResult ReactivateRestaurant([FromQuery] int? id)
        {
            //id z tokenu po zalogowaniu
            _restaurantService.ReactivateRestaurant(id, Account.Id);
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
        [HttpPost("deactivate")]
        [Authorize(Role.admin, Role.restaurateur)]
        public ActionResult DeactivateRestaurant([FromQuery] int? id)
        {
            //id z tokenu po zalogowaniu
            _restaurantService.DeactivateRestaurant(id, Account.Id);
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
        [Authorize(Role.admin)]
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
        [Authorize(Role.admin)]
        public ActionResult UnblockRestaurant([FromQuery] int id)
        {
            _restaurantService.UnblockRestaurant(id);
            return Ok();
        }
    }
}
