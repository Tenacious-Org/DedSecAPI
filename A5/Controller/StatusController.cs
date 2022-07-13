using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly StatusService _statusService;
        public StatusController(ILogger<StatusController> logger,StatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        } 
        [HttpGet("GetById")]
        public ActionResult GetByStatusId([FromQuery] int id)
        {
            try{
                var data = _statusService.GetById(id);
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("StatusController : GetByStatusId(int id) : (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
            
        }

            

    }
}
      
        
