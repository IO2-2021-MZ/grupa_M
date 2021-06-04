using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using webApi.DataTransferObjects.ReviewDTO;
using webApi.Services;
using webApi.Enums;

namespace webApi.Controllers
{
    [ApiController]
    [Route("review")]
    [EnableCors("AllowOrigin")]
    public class ReviewController : AuthenticativeController
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur)]
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur)]
        public ActionResult CreateReview([FromBody] NewReview newReview)
        {
            // Mapping example
            int id = _reviewService.CreateNewReview(newReview);
            return Ok($"/review/{id}");
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
        [Authorize(Role.admin, Role.restaurateur)]
        public ActionResult DeleteReview([FromQuery] int id)
        {
            // Mapping example
            _reviewService.DeleteReview(id);
            return Ok();
        }
    }
}
