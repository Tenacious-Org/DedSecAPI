using System;
using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly DesignationService _designationService;
        public DesignationController(DesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAllDesignation()
        {
            var data = _designationService.GetAll();
            return Ok(data);
        }

        [HttpGet("GetDesignationsByDepartmentId")]
        public ActionResult GetDesignationsByDepartmentId(int id)
        {
            var data = _designationService.GetDesignationsByDepartmentId(id);
            return Ok(data);
        }

        [HttpGet("GetById")]
        public ActionResult GetByDesignationId([FromQuery] int id)
        {
            var data = _designationService.GetById(id);
            return Ok(data);
        }

        [HttpPost("Create")]
        public ActionResult Create(Designation designation)
        {
            var data = _designationService.Create(designation);
            if(data){
                return Ok("Created.");
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public ActionResult Update(Designation designation, int id)
        {
            var data = _designationService.Update(designation, id);
            if(data){
                return Ok("Updated.");
            }
            return BadRequest();
        }

        [HttpPut("Disable")]
        public ActionResult Disable(Designation designation, int id)
        {
            var data = _designationService.Disable(designation, id);
            if(data)
            {
                return Ok("Disabled.");
            }
            return BadRequest();
        }
    }
}