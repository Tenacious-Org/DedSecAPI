using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Data.Service;
using System.ComponentModel.DataAnnotations;

namespace A5.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly ILogger<AwardService> _logger;
        private readonly AwardService _awardService;

        public AwardController(ILogger<AwardService> logger,AwardService awardService)
        {
            _awardService=awardService;
            _logger=logger;
        }
        [HttpPost("RaiseRequest")]
        public ActionResult RaiseRequest(Award award)
        {
            try{
                var data=_awardService.RaiseRequest(award);
                return Ok(data);
            }           
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            
        }
        [HttpPut("Approve")]
        public ActionResult Approve(Award award,int id){
        
            
            try{
                var data=_awardService.Approve(award,id);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

        }
         [HttpPut("Reject")]
        public ActionResult Reject(Award award,int id){
        
            
            try{
                var data=_awardService.Reject(award,id);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

        }
       
       
        [HttpPut("Publish")]
        public ActionResult Publish(Award award,int id)
        {
            
            try{
                var data=_awardService.Publish(award,id);
                return Ok(data);
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

        }
        
       [HttpGet("GetRequestedAward")]
       public ActionResult GetRequestedAward(int employeeId)
       {
            
            try{
                var data=_awardService.GetRequestedAward(employeeId);
                return Ok(data);
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
        [HttpGet("GetAwards")]
       public ActionResult GetAwards(int statusId,int employeeId)
       {
           
           try{
                var data=_awardService.GetAwards(statusId,employeeId);
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

       [HttpGet("GetAward")]
       public ActionResult GetAward(int id)
       {
           
           try{
                var data=_awardService.GetAward(id);
                return Ok(data);
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
       [HttpPost("AddComment")]
       public ActionResult AddComment(Comment comment)
       {
           
           try{
               var data=_awardService.AddComment(comment);
                return Ok(data);               
            }
             catch(ValidationException exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
       [HttpGet("GetComments")]
       public ActionResult GetComments(int awardId)
       {
           
           try{
                var data=_awardService.GetComments(awardId);
                return Ok(data);
            }
            catch(Exception exception)
            {
                _logger.LogError($"log: (Error: {exception.Message})");
                return BadRequest($"Error : {exception.Message}");
            }

       }
    }
}




















