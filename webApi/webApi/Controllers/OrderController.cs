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
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Returns Order Details
        /// </summary>
        /// <param name="id"> Order Id </param>
        /// <returns> Returns Order Details </returns>
        /// <response code="200">Return order details</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpGet]
        public ActionResult<TransferOrder> GetOrder([FromQuery] int? id)
        {
            TransferOrder order = _orderService.GetOrderById(id);
            return Ok(order);
        }

        /// <summary>
        /// Creates New Order
        /// </summary>
        /// <returns> Creates New Order </returns>
        /// <response code="200">Order successfully created</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        public ActionResult CreateOrder([FromBody] NewOrder newOrder)
        {
            int id = _orderService.CreateNewOrder(newOrder);
            return Created($"/order/{id}", null);
        }

        /// <summary>
        /// Refuses Order
        /// </summary>
        /// <param name="id"> Order Id </param>
        /// <returns> Refuses Order </returns>
        /// <response code="200">Order refused</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("refuse")]
        public ActionResult RefuseOrder([FromQuery] int id)
        {
            _orderService.RefuseOrder(id);
            return Ok();
        }

        /// <summary>
        /// Accepts Order
        /// </summary>
        /// <param name="id"> Order Id </param>
        /// <returns> Accepts Order </returns>
        /// <response code="200">Order accepted</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("accept")]
        public ActionResult AcceptOrder([FromQuery] int id)
        {
            _orderService.AcceptOrder(id);
            return Ok();
        }

        /// <summary>
        /// Marks Order As Realized
        /// </summary>
        /// <param name="id"> Order Id </param>
        /// <returns> Marks Order As Realized </returns>
        /// <response code="200">Order has been marked as realized</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("realized")]
        public ActionResult RealiseOrder([FromQuery] int id)
        {
            // TODO: DTO OrderA, OrderR, OrderC
            _orderService.RealiseOrder(id);
            return Ok();
        }
    }
}
