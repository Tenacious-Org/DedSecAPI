using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardTypeController : ControllerBase
    {
        private readonly AwardTypeService _awardTypeService;

        public AwardTypeController(AwardTypeService awardTypeService)
        {
            _awardTypeService = awardTypeService;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var data = _awardTypeService.GetAll();
            return Ok(data);
        }

        [HttpGet("GetById")]
        public ActionResult GetById(int id)
        {
            var data = _awardTypeService.GetById(id);
            return Ok(data);
        }

        [HttpPost("Create")]
        public ActionResult Create(AwardType awardType)
        {
            var data = _awardTypeService.Create(awardType);
            if(data)
            {
                return Ok("Created.");
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public ActionResult Update(AwardType awardType,int id)
        {
            var data =_awardTypeService.Update(awardType,id);
            if(data)
            {
                return Ok("Updated.");
            }
            return BadRequest();
        }

        [HttpPut("Disable")]
        public ActionResult Disable(AwardType awardType,int id)
        {
            var data = _awardTypeService.Disable(awardType,id);
            if(data)
            {
                return Ok("Disabled.");
            }
            return BadRequest();
        }

    }
}