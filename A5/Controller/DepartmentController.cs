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
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly AppDbContext _context;
        private readonly DepartmentService _departmentService;
        public DepartmentController(ILogger<DepartmentController> logger, AppDbContext context,DepartmentService departmentService)
        {
            _logger= logger;
            _context = context;
            _departmentService = departmentService;
        }

       
        [HttpGet("GetAll")]
        public ActionResult GetAllDepartment()
        {
            try
            {
                var result = (from o in _context.Organisations
              join d in _context.Departments on o.Id equals d.OrganisationId
              select new{
                 d.Id,
                 d.DepartmentName,
                 o.OrganisationName,
                 d.IsActive,
                 d.AddedBy,
                 d.AddedOn,
                 d.UpdatedBy,
                 d.UpdatedOn
             }).ToList();
                return Ok(result);
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

        [HttpGet("GetDepartmentsByOrganisationId")]
        public ActionResult GetDepartmentsByOrganisationId(int id)
        {
            try{
                var data = _departmentService.GetDepartmentsByOrganisationId(id);
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
        public ActionResult GetByDepartmentId([FromQuery] int id)
        {
            try{
                var data = _departmentService.GetById(id);
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
        public ActionResult Create(Department department)
        {
            try{
                var data = _departmentService.Create(department);
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
        public ActionResult Update(Department department)
        {
            try{
                var data = _departmentService.Update(department);
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
        public ActionResult Disable(int id)
        {
            try{
                var data = _departmentService.Disable(id);
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