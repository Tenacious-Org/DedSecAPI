using System;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly OrganisationService _organisationService;
        public OrganisationController(ILogger<OrganisationController> logger,OrganisationService organisationService)
        {
            _logger = logger;
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
        public ActionResult Update(Organisation organisation, int id)
        {
            try{
                var data = _organisationService.Update(organisation, id);           
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
        public ActionResult Disable(Organisation organisation, int id)
        {
            try{
                var data = _organisationService.Disable(organisation, id);          
                return Ok("Organisation Disabled.");
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