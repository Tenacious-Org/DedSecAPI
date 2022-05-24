using System;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;
using A5.Data;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly AppDbContext _context;
        private readonly OrganisationService _organisationService;
        public OrganisationController(ILogger<OrganisationController> logger, AppDbContext context, OrganisationService organisationService)
        {
            _logger = logger;
            _context = context;
            _organisationService = organisationService;
        } 

        [HttpGet("GetAll")]
        public ActionResult GetAllOrganisation()
        {
            try{
                var data = _organisationService.GetAll();
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
            
        }


         [HttpGet("GetById")]
        public ActionResult GetByOrganisationId([FromQuery] int id)
        {
            try{
                var data = _organisationService.GetById(id);
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
            
        }

        [HttpPost("Create")]
        public ActionResult Create(Organisation organisation)
        {
            try
            {    
                var data = _organisationService.Create(organisation);
                return Ok("Organisation Created.");
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }

        [HttpPut("Update")]
        public ActionResult Update(Organisation organisation)
        {
            try{
                var data = _organisationService.Update(organisation);           
                return Ok("Organisation Updated.");          
            }        
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            try
            {
                var checkEmployee = _context.Set<Employee>().Where(nameof =>nameof.IsActive == true && nameof.OrganisationId == id).ToList().Count();
                if(checkEmployee > 0)
                {
                    return Ok("There are certain Employees in that Organisation. You're gonna change their Organisation to disable this Organisation.");
                }
                else
                {
                    var data = _organisationService.Disable(id);
                    return Ok(data);
                }
            }
            catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                return BadRequest($"Error : {exception.Message}");
            }
        }
    }
}