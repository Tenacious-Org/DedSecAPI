using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Route("[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<IStatusService> _logger;
        private readonly IStatusService _statusService;
        public StatusController(ILogger<IStatusService> logger, IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }

        /// <summary>
        ///  This Method is used to view status by Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / GetById
        ///     {
        ///        "Id" = "2",  
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="statusId">String</param>
        /// <returns>
        ///Returns status from Id
        /// </returns>

        [HttpGet("GetById")]
        public ActionResult GetByStatusId([FromQuery] int statusId)
        {
            if(statusId<=0) throw new ValidationException("Status id should not be zero or negative");
            try
            {
                var data = _statusService.GetStatusById(statusId);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("Status Controller : GetByStatusId(id : {id}) : (Error: {exception.Message})",statusId,exception.Message);
                return BadRequest((exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("Status Controller : GetByStatusId(id : {id}) : (Error: {exception.Message})",statusId,exception.Message);
                return Problem($"Error : {exception.Message}");
            }

        }

        /// <summary>
        ///  This Method is used to view all status.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / GetAll
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param >String</param>
        /// <returns>
        ///Returns List of Status
        /// </returns>


        [HttpGet("GetAll")]
        public ActionResult GetAllStatus()
        {
            try
            {
                var data = _statusService.GetAllStatus();
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("StatusController : GetAllStatus() : (Error:{Message})", exception.Message);
                return BadRequest((exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError("StatusController : GetAllStatus() : (Error:{Message})", exception.Message);
                return Problem(exception.Message);
            }

        }



    }
}


