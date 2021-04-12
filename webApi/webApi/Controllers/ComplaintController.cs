using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("complaint")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        /// <summary>
        /// Returns Complaint Details
        /// </summary>
        /// <param name="id"> Complaint Id </param>
        /// <returns> Returns Complaint Details </returns>
        /// <response code="200">Return restaurant details</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ComplaintDTO> GetComplaint([FromQuery] int? id)
        {
            ComplaintDTO complaint = _complaintService.GetComplaintById(id);
            if(complaint == null)
            {
                return NotFound("Resource not Found");
            }
            return Ok(complaint);
        }

        /// <summary>
        /// Creates New Complaint
        /// </summary>
        /// <returns> Create New Complaint</returns>
        /// <response code="200">Complaint successfully added</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult CreateComplaint([FromBody] NewComplaint newComplaint)
        {
            int id = _complaintService.CreateNewComplaint(newComplaint);
            return Ok();
        }

        /// <summary>
        /// Deletes Complaint
        /// </summary>
        /// <param name="id"> Complaint Id </param>
        /// <returns> Delete Restaurant </returns>
        /// <response code="200">Complaint deleted</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteComplaint([FromQuery] int id)
        {
            _complaintService.DeleteComplaint(id);
            return Ok();
        }

        /// <summary>
        /// Returns All Complaints
        /// </summary>
        /// <returns> Returns All Complaints </returns>
        /// <response code="200">Returns all complaints </response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ComplaintDTO>> GetAllComplaints()
        {
            IEnumerable<ComplaintDTO> complaints = _complaintService.GetAllComplaints();
            return Ok(complaints);
        }

        /// <summary>
        /// Closes complaint
        /// </summary>
        /// <param name="id"> Complaint Id </param>
        /// <returns> Closes Complaint </returns>
        /// <response code="200">Complaint closed</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("block")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult CloseComplaint([FromQuery] int id)
        {
            _complaintService.CloseComplaint(id);
            return Ok();
        }

    }
}


