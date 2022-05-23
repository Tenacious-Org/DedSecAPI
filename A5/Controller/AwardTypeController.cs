using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardTypeController : ControllerBase
    {
        private readonly AwardTypeService _awardTypeService;
        private readonly ILogger<AwardTypeController> _logger;
        public AwardTypeController( ILogger<AwardTypeController> logger,AwardTypeService awardTypeService)
        {
            _awardTypeService = awardTypeService;
            _logger=logger;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            try{
                var data = _awardTypeService.GetAll();
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
        public ActionResult GetById(int id)
        {
            try
            {
                var data = _awardTypeService.GetById(id);
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
        public ActionResult Create(AwardType awardType)
        {
            try{
                var data = _awardTypeService.Create(awardType);
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

        [HttpPut("Update")]
        public ActionResult Update(AwardType awardType)
        {
            try{
                var data = _awardTypeService.Update(awardType);
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

        [HttpPut("Disable")]
        public ActionResult Disable(AwardType awardType, int id)
        {
            try{
                var data = _awardTypeService.Disable(awardType,id);
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

    }
}