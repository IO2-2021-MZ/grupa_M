using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.Services;
using webApi.Enums;

namespace webApi.Controllers
{
    [ApiController]
    [Route("discountCode")]
    [EnableCors("AllowOrigin")]
    public class DiscountCodeController : AuthenticativeController
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur, Role.employee)]
        public ActionResult<DiscountCodeDTO> GetDiscountCode([FromQuery] string? code)
        {
            var discountCode = _discountCodeService.GetDiscountCodeByCode(code, Account.Id);
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
        [Authorize(Role.admin, Role.restaurateur, Role.employee)]
        public ActionResult CreateDiscountCode([FromBody] NewDiscountCode newDiscountCode)
        {
            int id = _discountCodeService.CreateNewDiscountCode(newDiscountCode, Account.Id);
            return Ok($"/discountCode/{id}");
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
        [Authorize(Role.admin, Role.restaurateur, Role.employee)]
        public ActionResult DeleteDiscountCode([FromQuery] int id)
        {
            // Mapping example
            _discountCodeService.DeleteDiscountCode(id, Account.Id);
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur, Role.employee)]
        public ActionResult<IEnumerable<DiscountCodeDTO>> GetAllDiscountCodes()
        {
            IEnumerable<DiscountCodeDTO> discountCodes = _discountCodeService.GetAllDiscountCodes(Account.Id);
            return Ok(discountCodes);
        }

    }
}
