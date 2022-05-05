using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

         [HttpGet("GetAll")]
        public ActionResult GetAllEmployees()
        {
            var data = _employeeService.GetAll();
            return Ok(data);
        }


        [HttpGet("GetById")]
        public ActionResult GetByOrganisationId([FromQuery] int id)
        {
            var data = _employeeService.GetById(id);
            return Ok(data);
        }

        [HttpPost("Create")]
        public ActionResult Create(Employee employee)
        {
            try
            {    
                var data = _employeeService.Create(employee);
                return Ok(data);
            }
            catch(ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("Update")]
        public ActionResult Update(Employee employee, int id)
        {
            var data = _employeeService.Update(employee, id);
            if(data){
                return Ok("Updated.");
            }
            return BadRequest();
        }






    }
}