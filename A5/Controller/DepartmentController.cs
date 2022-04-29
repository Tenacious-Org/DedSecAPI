using System;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpGet("GetAll")]
        public ActionResult GetAllDepartment()
        {
            var data = _departmentService.GetAll();
            return Ok(data);
        }

        [HttpGet("GetById")]
        public ActionResult GetByDepartmentId([FromQuery] int id)
        {
            var data = _departmentService.GetById(id);
            return Ok(data);
        }

        [HttpPost("Create")]
        public ActionResult Create(Department department)
        {
            var data = _departmentService.Create(department);
            if(data){
                return Ok("Created.");
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public ActionResult Update(Department department, int id)
        {
            var data = _departmentService.Update(department, id);
            if(data){
                return Ok("Updated.");
            }
            return BadRequest();
        }

        [HttpDelete("Disable")]
        public ActionResult Disable(Department department, int id)
        {
            var data = _departmentService.Disable(department, id);
            if(data)
            {
                return Ok("Disabled.");
            }
            return BadRequest();
        }
    }
}