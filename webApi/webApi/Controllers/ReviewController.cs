using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.Models;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("review")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Returns Review Details
        /// </summary>
        /// <param name="id"> Review Id </param>
        /// <returns> Returns Review Details </returns>
        /// <response code="200">Return Review details</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpGet]
        public ActionResult<ReviewDTO> GetReview([FromQuery] int? id)
        {
            var review = _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound("Resource not Found");
            }
            return Ok(review);
        }

        /// <summary>
        /// Creates New Review
        /// </summary>
        /// <returns> Create New Review</returns>
        /// <response code="200">Review successfully added</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        [HttpPost]
        public ActionResult CreateReview([FromBody] NewReview newReview)
        {
            // Mapping example
            _reviewService.CreateNewReview(newReview);
            return Ok();
        }

        /// <summary>
        /// Deletes Review
        /// </summary>
        /// <param name="id"> Review Id </param>
        /// <returns> Delete Review </returns>
        /// <response code="200">Review deleted</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpDelete]
        public ActionResult DeleteReview([FromQuery] int id)
        {
            // Mapping example
            _reviewService.DeleteReview(id);
            return Ok();
        }
    }
}
