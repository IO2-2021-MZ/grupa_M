using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.Dish;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.Restaurant;
using webApi.DataTransferObjects.Review;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("discountCode")]
    public class DiscountCodeController : ControllerBase
    {
        private readonly IDiscountCodeService _discountCodeService;

        public DiscountCodeController(IDiscountCodeService discountCodeService)
        {
            _discountCodeService = discountCodeService;
        }

        /// <summary>
        /// Returns DiscountCode Details
        /// </summary>
        /// <param name="id"> DiscountCode Id </param>
        /// <returns> Returns DiscountCode Details </returns>
        /// <response code="200">Return DiscountCode details</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpGet]
        public ActionResult<TransferDiscountCode> GetDiscountCode([FromQuery] int? id)
        {
            var discountCode = _discountCodeService.GetDiscountCodeById(id);
            if (discountCode == null)
            {
                return NotFound("Resource not Found");
            }
            return Ok(discountCode);
        }

        /// <summary>
        /// Creates New DiscountCode
        /// </summary>
        /// <returns> Create New DiscountCode</returns>
        /// <response code="200">DiscountCode successfully added</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        [HttpPost]
        public ActionResult CreateReview([FromBody] NewDiscountCode newDiscountCode)
        {
            // Mapping example
            _discountCodeService.CreateNewDiscountCode(newDiscountCode);
            return Ok();
        }

        /// <summary>
        /// Deletes DiscountCode
        /// </summary>
        /// <param name="id"> DiscountCode Id </param>
        /// <returns> Delete DiscountCode </returns>
        /// <response code="200">DiscountCode deleted</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpDelete]
        public ActionResult DeleteDiscountCode([FromQuery] int id)
        {
            // Mapping example
            _discountCodeService.DeleteDiscountCode(id);
            return Ok();
        }

        /// <summary>
        /// Returns All DiscountCodes
        /// </summary>
        /// <returns> Returns All DiscountCodes </returns>
        /// <response code="200">Returns all DiscountCodes </response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpGet("all")]
        public ActionResult<IEnumerable<TransferDiscountCode>> GetAllDiscountCodes()
        {
            IEnumerable<TransferDiscountCode> discountCodes = _discountCodeService.GetAllDiscountCodes();
            return Ok(discountCodes);
        }

    }
}
