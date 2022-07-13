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
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly RoleService _roleService;
        public RoleController(ILogger<RoleController> logger,RoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        } 

        [HttpGet("GetById")]
        public ActionResult GetByRoleId([FromQuery] int id)
        {
            try{
                var data = _roleService.GetById(id);
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("RoleController : GetByRoleId(int id) : (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }            
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            try{
                var data = _roleService.GetAll();
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                 _logger.LogError("RoleController : GetAll() : (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }          
        }    

    }
} 
        
