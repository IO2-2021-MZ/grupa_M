﻿using Microsoft.AspNetCore.Cors;
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur, Role.employee)]
        public ActionResult<OrderDTO> GetOrder([FromQuery] int? id)
        {
            OrderDTO order = _orderService.GetOrderById(id, Account.Id);
            return Ok(order);
        }

        /// <summary>
        /// Returns Order Archive
        /// </summary>
        /// <returns> Returns All Orders </returns>
        /// <response code="200">Return orders</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response>
        [HttpGet("archive")]
        [Authorize(Role.admin)]
        public ActionResult<IEnumerable<OrderA>> GetOrdersArchive()
        {
            IEnumerable<OrderA> orders = _orderService.GetOrdersArchive();
            return Ok(orders);
        }

        /// <summary>
        /// Creates New Order
        /// </summary>
        /// <returns> Creates New Order </returns>
        /// <response code="200">Order successfully created</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Authorize(Role.customer)]
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
        [Authorize(Role.restaurateur, Role.employee)]
        public ActionResult RefuseOrder([FromQuery] int id)
        {
            _orderService.RefuseOrder(id, Account.Id);
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
        [Authorize(Role.restaurateur, Role.employee)]
        public ActionResult AcceptOrder([FromQuery] int id)
        {
            _orderService.AcceptOrder(id, Account.Id);
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
        [Authorize(Role.restaurateur, Role.employee)]
        public ActionResult RealiseOrder([FromQuery] int id)
        {
            _orderService.RealiseOrder(id, Account.Id);
            return Ok();
        }
    }
}
