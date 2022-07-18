using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Route("[controller]")]
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
        [HttpGet("GetById")]
        public ActionResult GetByStatusId([FromQuery] int id)
        {
            try
            {
                var data = _statusService.GetStatusById(id);
                return Ok(data);
            }
            catch (ValidationException exception)
            {
                _logger.LogError($"Status Controller : GetByStatusId(int id) : (Error: {exception.Message})");
                return BadRequest((exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Status Controller : GetByStatusId(int id) : (Error: {exception.Message})");
                return Problem($"Error : {exception.Message}");
            }

        }
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


