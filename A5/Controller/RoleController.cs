using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Route("api/[controller]")]
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
                _logger.LogError($"log: (Error: {exception.Message})");
                _logger.LogInformation($"Role Controller : GetByRoleId(int id) : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
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
                _logger.LogError($"log: (Error: {exception.Message})");
                 _logger.LogInformation($"Role Controller : GetAll() : (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }          
        }    

    }
} 
        
