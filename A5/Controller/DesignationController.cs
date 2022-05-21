using System;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly ILogger<DesignationController> _logger;
        private readonly DesignationService _designationService;
        public DesignationController( ILogger<DesignationController> logger, DesignationService designationService)
        {
            _logger = logger;
            _designationService = designationService;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAllDesignation()
        {
            try{
                var data = _designationService.GetAll();
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

        [HttpGet("GetDesignationsByDepartmentId")]
        public ActionResult GetDesignationsByDepartmentId(int id)
        {
            try{
                var data = _designationService.GetDesignationsByDepartmentId(id);
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
        public ActionResult GetByDesignationId([FromQuery] int id)
        {
            try{
                var data = _designationService.GetById(id);
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
        public ActionResult Create(Designation designation)
        {
            try{
                var data = _designationService.Create(designation);           
                return Ok("Created.");
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
        public ActionResult Update(Designation designation)
        {
            try{
                var data = _designationService.Update(designation);
                return Ok("Updated.");
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
        public ActionResult Disable(Designation designation, int id)
        {


            try
            {
                var data = _designationService.Disable(designation, id);
                return Ok("Disabled.");
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