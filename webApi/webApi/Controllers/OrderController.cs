using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using webApi.DataTransferObjects.OrderDTO;
using webApi.Enums;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("order")]
    [EnableCors("AllowOrigin")]
    public class OrderController : AuthenticativeController
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
        [Authorize(Role.Admin, Role.Customer, Role.Restaurer)]
        public ActionResult<OrderDTO> GetOrder([FromQuery] int? id)
        {
            OrderDTO order = _orderService.GetOrderById(id, Account.Id);
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
        [Authorize(Role.Customer)]
        public ActionResult CreateOrder([FromBody] NewOrder newOrder)
        {
            int id = _orderService.CreateNewOrder(newOrder, Account.Id);
            return Ok($"/order/{id}");
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
            _orderService.RealiseOrder(id);
            return Ok();
        }
    }
}
