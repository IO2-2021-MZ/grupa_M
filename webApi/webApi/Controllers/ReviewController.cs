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
        public ActionResult<TransferReview> GetReview([FromQuery] int? id)
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
            return Ok();
        }
    }
}
