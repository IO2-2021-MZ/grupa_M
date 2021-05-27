using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using webApi.DataTransferObjects.ComplaintDTO;
using webApi.Enums;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("complaint")]
    public class ComplaintController : AuthenticativeController
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
        [Authorize(Role.admin, Role.customer, Role.restaurateur, Role.employee)]
        public ActionResult<ComplaintDTO> GetComplaint([FromQuery] int? id)
        {
            ComplaintDTO complaint = _complaintService.GetComplaintById(id, Account.Id);
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
        [Authorize(Role.customer)]
        public ActionResult CreateComplaint([FromBody] NewComplaint newComplaint)
        {
            int id = _complaintService.CreateNewComplaint(newComplaint, Account.Id);
            return Ok($"/complaint/{id}");
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
        [Authorize(Role.admin)]
        public ActionResult DeleteComplaint([FromQuery] int id)
        {
            _complaintService.DeleteComplaint(id, Account.Id);
            return Ok();
        }


        /// <summary>
        /// Closes complaint
        /// </summary>
        /// <param name="id"> Complaint Id </param>
        /// <param name="content"> Complaint response </param>
        /// <returns> Closes Complaint </returns>
        /// <response code="200">Complaint closed</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response> 
        /// <response code="404">Resource Not Found</response> 
        [HttpPost("respond")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Role.employee, Role.restaurateur)]
        public ActionResult ResponsecComplaint([FromQuery] int id, [FromBody] string content)
        {
            _complaintService.CloseComplaint(id, content, Account.Id);
            return Ok();
        }

    }
}


